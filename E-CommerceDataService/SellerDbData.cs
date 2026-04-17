using E_CommerceModels;
using Microsoft.Data.SqlClient;

namespace E_CommerceDataService
{
    public class SellerDbData : INTSellerDataService
    {
        private string connectionString
        = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=sellerMgmt;Integrated Security=True;TrustServerCertificate=True;";

        private SqlConnection sqlConnection;

        public SellerDbData()
        {
            sqlConnection = new SqlConnection(connectionString);
            AddData();
        }

        private void AddData()
        {
            var existing = GetSellers();

            if (existing.Count == 0)
            {
                Product Bakery1 = new Product { ProductID = Guid.NewGuid(), ProductName = "Crinkles", ProductPrice = 7.00 };
                Product Bakery2 = new Product { ProductID = Guid.NewGuid(), ProductName = "Brownie", ProductPrice = 49.99 };

                SellerProfile Almadin_Bakery = new SellerProfile();
                Almadin_Bakery.SellerID = Guid.NewGuid();
                Almadin_Bakery.SellerName = "Bake my Day";
                Almadin_Bakery.ProductName = new List<Product>();
                Almadin_Bakery.ProductName.Add(Bakery1);
                Almadin_Bakery.ProductName.Add(Bakery2);

                Product Art1 = new Product { ProductID = Guid.NewGuid(), ProductName = "Paint Brush", ProductPrice = 79.99 };
                Product Art2 = new Product { ProductID = Guid.NewGuid(), ProductName = "Canvas", ProductPrice = 99.99 };
                Product Art3 = new Product { ProductID = Guid.NewGuid(), ProductName = "Paint", ProductPrice = 29.99 };

                SellerProfile ArtStore = new SellerProfile();
                ArtStore.SellerID = Guid.NewGuid();
                ArtStore.SellerName = "Paints & Pains";
                ArtStore.ProductName = new List<Product>();
                ArtStore.ProductName.Add(Art1);
                ArtStore.ProductName.Add(Art2);
                ArtStore.ProductName.Add(Art3);

                Add(Almadin_Bakery);
                Add(ArtStore);
            }
        }

        // CREATE
        public void Add(SellerProfile seller)
        {
            var insertSeller = "INSERT INTO SellersTbl VALUES (@SellerID, @SellerName)";
            SqlCommand sellerCommand = new SqlCommand(insertSeller, sqlConnection);
            sellerCommand.Parameters.AddWithValue("@SellerID", seller.SellerID);
            sellerCommand.Parameters.AddWithValue("@SellerName", seller.SellerName);

            sqlConnection.Open();
            sellerCommand.ExecuteNonQuery();
            sqlConnection.Close();

            foreach (var product in seller.ProductName)
            {
                var insertProduct = "INSERT INTO ProductsTbl VALUES (@ProductID, @ProductName, @ProductPrice, @SellerID)";
                SqlCommand productCommand = new SqlCommand(insertProduct, sqlConnection);
                productCommand.Parameters.AddWithValue("@ProductID", product.ProductID);
                productCommand.Parameters.AddWithValue("@ProductName", product.ProductName);
                productCommand.Parameters.AddWithValue("@ProductPrice", product.ProductPrice);
                productCommand.Parameters.AddWithValue("@SellerID", seller.SellerID);

                sqlConnection.Open();
                productCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        // READ
        public List<SellerProfile> GetSellers()
        {
            string getSeller = "SELECT * FROM SellersTbl";
            SqlCommand selectCommand = new SqlCommand(getSeller, sqlConnection);

            sqlConnection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();

            var sellers = new List<SellerProfile>();

            while (reader.Read())
            {
                SellerProfile seller = new SellerProfile();
                seller.SellerID = Guid.Parse(reader["SellerID"].ToString());
                seller.SellerName = reader["SellerName"].ToString();
                seller.ProductName = new List<Product>();
                sellers.Add(seller);
            }

            sqlConnection.Close();

            foreach (var seller in sellers)
            {
                seller.ProductName = GetProductsBySellerId(seller.SellerID);
            }

            return sellers;
        }

        // GET PRODUCTS BY SELLER ID
        private List<Product> GetProductsBySellerId(Guid sellerID)
        {
            string getProduct = "SELECT * FROM ProductsTbl WHERE SellerID = @SellerID";
            SqlCommand selectCommand = new SqlCommand(getProduct, sqlConnection);
            selectCommand.Parameters.AddWithValue("@SellerID", sellerID);

            sqlConnection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();

            var products = new List<Product>();

            while (reader.Read())
            {
                Product product = new Product();
                product.ProductID = Guid.Parse(reader["ProductID"].ToString());
                product.ProductName = reader["ProductName"].ToString();
                product.ProductPrice = double.Parse(reader["ProductPrice"].ToString());
                products.Add(product);
            }

            sqlConnection.Close();
            return products;
        }

        // READ - GUID
        public SellerProfile? GetbyId(Guid ID)
        {
            var idSeller = "SELECT * FROM SellersTbl WHERE SellerID = @SellerID";
            SqlCommand selectCommand = new SqlCommand(idSeller, sqlConnection);
            selectCommand.Parameters.AddWithValue("@SellerID", ID);

            sqlConnection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();

            SellerProfile seller = new SellerProfile();

            while (reader.Read())
            {
                seller.SellerID = Guid.Parse(reader["SellerID"].ToString());
                seller.SellerName = reader["SellerName"].ToString();
            }

            sqlConnection.Close();

            seller.ProductName = GetProductsBySellerId(seller.SellerID);
            return seller;
        }

        // READ - Name
        public SellerProfile? GetbyName(string sellerName)
        {
            var nameSeller = "SELECT * FROM SellersTbl WHERE SellerName = @SellerName";
            SqlCommand selectCommand = new SqlCommand(nameSeller, sqlConnection);
            selectCommand.Parameters.AddWithValue("@SellerName", sellerName);

            sqlConnection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();

            SellerProfile seller = new SellerProfile();

            while (reader.Read())
            {
                seller.SellerID = Guid.Parse(reader["SellerID"].ToString());
                seller.SellerName = reader["SellerName"].ToString();
            }

            sqlConnection.Close();

            seller.ProductName = GetProductsBySellerId(seller.SellerID);
            return seller;
        }

        // CHECK
        public bool SellerExists(string sellerName)
        {
            var existSellert = "SELECT * FROM SellersTbl WHERE SellerName = @SellerName";
            SqlCommand selectCommand = new SqlCommand(existSellert, sqlConnection);
            selectCommand.Parameters.AddWithValue("@SellerName", sellerName);

            sqlConnection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();

            SellerProfile seller = new SellerProfile();

            while (reader.Read())
            {
                seller.SellerName = reader["SellerName"].ToString();
            }

            sqlConnection.Close();
            return seller.SellerName != null;
        }

        // UPDATE
        public void Update(SellerProfile seller)
        {
            sqlConnection.Open();
            var updateSeller = "UPDATE SellersTbl SET SellerName = @SellerName WHERE SellerID = @SellerID";
            SqlCommand updateCommand = new SqlCommand(updateSeller, sqlConnection);
            updateCommand.Parameters.AddWithValue("@SellerName", seller.SellerName);
            updateCommand.Parameters.AddWithValue("@SellerID", seller.SellerID);

            updateCommand.ExecuteNonQuery();
            sqlConnection.Close();

            sqlConnection.Open();
            var deleteProducts = "DELETE FROM ProductsTbl WHERE SellerID = @SellerID";
            SqlCommand deleteCommand = new SqlCommand(deleteProducts, sqlConnection);
            deleteCommand.Parameters.AddWithValue("@SellerID", seller.SellerID);
            deleteCommand.ExecuteNonQuery();
            sqlConnection.Close();

            foreach (var product in seller.ProductName)
            {
                var insertProduct = "INSERT INTO ProductsTbl VALUES (@ProductID, @ProductName, @ProductPrice, @SellerID)";
                SqlCommand productCommand = new SqlCommand(insertProduct, sqlConnection);
                productCommand.Parameters.AddWithValue("@ProductID", product.ProductID);
                productCommand.Parameters.AddWithValue("@ProductName", product.ProductName);
                productCommand.Parameters.AddWithValue("@ProductPrice", product.ProductPrice);
                productCommand.Parameters.AddWithValue("@SellerID", seller.SellerID);

                sqlConnection.Open();
                productCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }


        // DELETE
        public void Delete(Guid ID)
        {

            sqlConnection.Open();
            var deleteProducts = "DELETE FROM ProductsTbl WHERE SellerID = @SellerID";
            SqlCommand deleteProductsCommand = new SqlCommand(deleteProducts, sqlConnection);
            deleteProductsCommand.Parameters.AddWithValue("@SellerID", ID);

            deleteProductsCommand.ExecuteNonQuery();
            sqlConnection.Close();

            sqlConnection.Open();
            var deleteSeller = "DELETE FROM SellersTbl WHERE SellerID = @SellerID";
            SqlCommand deleteSellerCommand = new SqlCommand(deleteSeller, sqlConnection);
            deleteSellerCommand.Parameters.AddWithValue("@SellerID", ID);

            deleteSellerCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }
    }
}