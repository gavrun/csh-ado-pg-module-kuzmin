// NpgsqlConnection 

Npgsql.NpgsqlConnection cnNorthwind =
  new Npgsql.NpgsqlConnection();

cnNorthwind.ConnectionString = 
  "Host=localhost;" +
  "Port=5432;" +
  "Username=sa;" +
  "Password=2389;" +
  "Database=Northwind;" +
  "Timeout=60;";







// OleDbConnection 

System.Data.OleDb.OleDbConnection cnNorthwind = new 
  System.Data.OleDb.OleDbConnection();

cnNorthwind.ConnectionString =
  "Provider=SQLOLEDB;" +
  "Data Source=ProdServ01;" +
  "Initial Catalog=Pubs;" +
  "Integrated Security=SSPI;";








// Creating a Connection Object, Opening,  and then Closing the Connection 

// Declare and instantiate a new NpgsqlConnection object

Npgsql.NpgsqlConnection cnNorthwind =
  new Npgsql.NpgsqlConnection();

// Set the ConnectionString property

cnNorthwind.ConnectionString = 
  "Host=localhost;" +
  "Port=5432;" +
  "Username=sa;" +
  "Password=2389;" +
  "Database=Northwind;";

// Open the connection

cnNorthwind.Open();

// perform some database task 

// Close the connection, which releases the connection to
// the connection pool on the server

cnNorthwind.Close();

// Release the memory taken by the NpgsqlConnection object
// (the memory will not be reclaimed until the Garbage
// Collector next executes)

cnNorthwind = null;








// Handling Connection Events

// the following code is usually added to the constructor
// for the class so that the function is linked to the
// appropriate event

this.cnNorthwind.StateChange += new 
  System.Data.StateChangeEventHandler(
  this.cnNorthwind_StateChange);


private void cnNorthwind_StateChange(
  object sender,
  System.Data.StateChangeEventArgs e)
{
  MessageBox.Show(
    "CurrentState: " + e.CurrentState.ToString() + "\n" +
    "OriginalState: " + e.OriginalState.ToString(),
    "cnNorthwind.StateChange",
    MessageBoxButtons.OK,
    MessageBoxIcon.Information);
}







// Example of try-catch Block 

Npgsql.NpgsqlConnection cnNorthwind;

try
{
	cnNorthwind = new Npgsql.NpgsqlConnection();

	cnNorthwind.ConnectionString = 
		"Host=localhost;" +
		"Port=5432;" +
		"Username=sa;" +
		"Password=2389;" +
		"Database=Northwind;";

	cnNorthwind.Open();

	// perform some database task 
}
catch (System.InvalidOperationException XcpInvOp)
{
	MessageBox.Show("You must close the connection first");
}
catch (System.Exception Xcp) 
{
	MessageBox.Show(Xcp.ToString());
}
finally
{
	cnNorthwind.Close();
	cnNorthwind = null;
}







// Handling SQL Exceptions 

// Write the code to execute inside a try block

try
{
	this.cnNorthwind.ConnectionString = 
		"Host=localhost;" +
		"Port=5432;" +
		"Username=sa;" +
		"Password=2389;" +
		"Database=Northwind;";

	this.cnNorthwind.Open();
}
// Write a catch statement for SqlExceptions

catch (Npgsql.PostgresException XcpSQL)
{
	string sErrorMsg = XcpSQL.MessageText;

		MessageBox.Show(sErrorMsg, "PostgreSQL Error " +
			XcpSQL.SqlState, MessageBoxButtons.OK, 
			MessageBoxIcon.Error);
	}
}

catch (Npgsql.NpgsqlException XcpSQL)  
{
	string sErrorMsg = XcpSQL.MessageText;

		MessageBox.Show(sErrorMsg, "PostgreSQL Server Error " +
			XcpSQL.ErrorCode, MessageBoxButtons.OK, 
			MessageBoxIcon.Error);

	}
}
// Write a handler for any other specific exceptions you want to catch

catch (System.InvalidOperationException XcpInvOp) 
{
	MessageBox.Show("Close the connection first!", 
		"Invalid Operation", 
		MessageBoxButtons.OK, MessageBoxIcon.Error);

}
// Write a generic catch statement for all other exceptions 

catch (System.Exception Xcp)  // generic exception handler
{
	MessageBox.Show(Xcp.Message, "Unexpected Exception", 
		MessageBoxButtons.OK, MessageBoxIcon.Error);
}
// Write a finally block after the Catch blocks

finally // run this code in every case
{
// write cleanup code here
}








// Creating Connection Pooling

// Connection 1

NpgsqlConnection myConnection = new NpgsqlConnection();
myConnection.ConnectionString = "User ID=sa;" +
  "Password=me2I81sour2;" +
  "Database=Northwind;" +
  "Host=myPostgreSQLServer;" +
  "Port=5432;" +
  "Timeout=30;";

// Connection 2

NpgsqlConnection myConnection = new NpgsqlConnection();
myConnection.ConnectionString = "User ID=sa;" +
  "Password=me2I81sour2;"+
  "Database=Northwind;"+
  "Host=myPostgreSQLServer;" +
  "Port=5432;" +
  "Timeout=30;";

// Connection 3

NpgsqlConnection myConnection = new NpgsqlConnection();
myConnection.ConnectionString = "User ID=sa;" +
  "Password=me2I81sour2;" +
  "Database=Pubs;" +
  "Host=myPostgreSQLServer;" +
  "Port=5432;" +
  "Timeout=30;";








// Controlling Connection Pooling

// To disable connection pooling

SqlClient.SqlConnection cnNorthwind = new SqlClient.SqlConnection();

cnNorthwind.ConnectionString = "User ID=sa;" +
  "Password=me2I81sour2;" +
  "Database=Northwind;" +
  "Host=myPostgreSQLServer;" +
  "Port=5432;" +
  "Pooling=False;";

// To specify the minimum pool size

cnNorthwind.ConnectionString = "User ID=sa;" +
  "Password=me2I81sour2;" +
  "Database=Northwind;" +
  "Host=myPostgreSQLServer;" +
  "Port=5432;" +
  "Minimum Pool Size=5;";

// To specify the connection lifetime
 
cnNorthwind.ConnectionString = "User ID=sa;" +
  "Password=me2I81sour2;" +
  "Database=Northwind;" +
  "Host=myPostgreSQLServer;" +
  "Port=5432;" +
  "Connection Idle Lifetime=120;";










//   Code for Specifying Command Parameters

/* Stored procedure with an input parameter named @CatID,
   an output parameter named @CatName, and a return value */

CREATE OR REPLACE FUNCTION CountProductsInCategory(
    CatID integer,
    OUT CatName character varying(15),
    OUT ProdCount integer
) AS $$
BEGIN
    SELECT Categories.CategoryName, 
           COUNT(Products.ProductID)
    INTO CatName, ProdCount
    FROM Categories
    INNER JOIN Products 
        ON Categories.CategoryID = Products.CategoryID
    WHERE Categories.CategoryID = CatID
    GROUP BY Categories.CategoryName;
    
    RETURN;
END;
$$ LANGUAGE plpgsql;


// Полный пример C# с использованием параметров для этой функции

using System;
using Npgsql;

public class Example
{
    public void GetCategoryInfo(int categoryId)
    {
        string connectionString = "Host=localhost;Port=5432;Username=sa;Password=me2I81sour2;Database=Northwind;";

        using (var connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();

            using (var command = new NpgsqlCommand("CountProductsInCategory", connection))
            {
                // Указываем, что это вызов хранимой функции
                command.CommandType = System.Data.CommandType.StoredProcedure;

                // Входной параметр
                command.Parameters.Add(new NpgsqlParameter("CatID", NpgsqlTypes.NpgsqlDbType.Integer));
                command.Parameters["CatID"].Value = categoryId;

                // Выходной параметр CatName
                var catNameParam = new NpgsqlParameter("CatName", NpgsqlTypes.NpgsqlDbType.Varchar, 15);
                catNameParam.Direction = System.Data.ParameterDirection.Output;
                command.Parameters.Add(catNameParam);

                // Выходной параметр ProdCount
                var prodCountParam = new NpgsqlParameter("ProdCount", NpgsqlTypes.NpgsqlDbType.Integer);
                prodCountParam.Direction = System.Data.ParameterDirection.Output;
                command.Parameters.Add(prodCountParam);

                // Выполняем функцию
                command.ExecuteNonQuery();

                // Чтение значений выходных параметров
                string categoryName = (string)catNameParam.Value;
                int productCount = (int)prodCountParam.Value;

                Console.WriteLine($"Category Name: {categoryName}");
                Console.WriteLine($"Product Count: {productCount}");
            }
        }
    }
}









// Code for Creating Parameters for a Command Object

NpgsqlParameter p1, p2, p3; 

p1 = new NpgsqlParameter("@RETURN_VALUE", SqlDbType.Int, 4);
p1.Direction = ParameterDirection.ReturnValue;

p2 = new NpgsqlParameter("@CatID", SqlDbType.Int, 4);
p2.Direction = ParameterDirection.Input;

p3 = new NpgsqlParameter("@CatName", SqlDbType.NChar, 15);
p3.Direction = ParameterDirection.Output;

cmdCountProductsInCategory.Parameters.Add(p1);
cmdCountProductsInCategory.Parameters.Add(p2);
cmdCountProductsInCategory.Parameters.Add(p3);


// Правильный код с комментариями для PostgreSQL

using Npgsql;
using NpgsqlTypes;
using System.Data;

NpgsqlParameter p1, p2, p3;

// Возвращаемое значение из функции (если функция действительно возвращает значение через RETURN)
p1 = new NpgsqlParameter("@RETURN_VALUE", NpgsqlDbType.Integer);
p1.Direction = ParameterDirection.ReturnValue;

// Входной параметр с категорией
p2 = new NpgsqlParameter("CatID", NpgsqlDbType.Integer);
p2.Direction = ParameterDirection.Input;

// Выходной параметр с названием категории
p3 = new NpgsqlParameter("CatName", NpgsqlDbType.Varchar, 15);
p3.Direction = ParameterDirection.Output;

// Добавляем параметры в команду
cmdCountProductsInCategory.Parameters.Add(p1);
cmdCountProductsInCategory.Parameters.Add(p2);
cmdCountProductsInCategory.Parameters.Add(p3);


// рабочий пример вызова функции с параметрами

using (var connection = new NpgsqlConnection("Host=localhost;Port=5432;Username=sa;Password=me2I81sour2;Database=Northwind;"))
{
    connection.Open();

    using (var cmdCountProductsInCategory = new NpgsqlCommand("CountProductsInCategory", connection))
    {
        cmdCountProductsInCategory.CommandType = CommandType.StoredProcedure;

        cmdCountProductsInCategory.Parameters.Add(new NpgsqlParameter("CatID", NpgsqlDbType.Integer) { Value = 1 });

        var catNameParam = new NpgsqlParameter("CatName", NpgsqlDbType.Varchar, 15)
        {
            Direction = ParameterDirection.Output
        };
        cmdCountProductsInCategory.Parameters.Add(catNameParam);

        var prodCountParam = new NpgsqlParameter("ProdCount", NpgsqlDbType.Integer)
        {
            Direction = ParameterDirection.Output
        };
        cmdCountProductsInCategory.Parameters.Add(prodCountParam);

        cmdCountProductsInCategory.ExecuteNonQuery();

        string categoryName = (string)catNameParam.Value;
        int productCount = (int)prodCountParam.Value;

        Console.WriteLine($"Category: {categoryName}, Products: {productCount}");
    }
}








// Code for Executing a Command That Returns a Single Value

string sql= "SELECT UnitsInStock FROM Products " +
                    "WHERE ProductID = @ProdID";

NpgsqlCommand cmProducts = new NpgsqlCommand(sql, cnNorthwind);

NpgsqlParameter param = cmProducts.Parameters.Add( 
		new NpgsqlParameter("@ProdID", SqlDbType.Int, 4));

cmProducts.Parameters["@ProdID"].Value = 42;

cnNorthwind.Open();

int qty  = Convert.ToInt32(cmProducts.ExecuteScalar());

cnNorthwind.Close();

MessageBox.Show("Quantity in stock: " + qty.ToString());



// исправленный и корректный код для PostgreSQL

string sql = "SELECT UnitsInStock FROM Products WHERE ProductID = @ProdID";

NpgsqlCommand cmProducts = new NpgsqlCommand(sql, cnNorthwind);

// Создаём параметр с правильным типом для PostgreSQL
NpgsqlParameter param = cmProducts.Parameters.Add(
    new NpgsqlParameter("ProdID", NpgsqlTypes.NpgsqlDbType.Integer)
);
param.Value = 42;

cnNorthwind.Open();

int qty = Convert.ToInt32(cmProducts.ExecuteScalar());

cnNorthwind.Close();

MessageBox.Show("Quantity in stock: " + qty.ToString());


// Пример использования

string connectionString = "Host=localhost;Port=5432;Username=sa;Password=me2I81sour2;Database=Northwind;";

using (var cnNorthwind = new NpgsqlConnection(connectionString))
{
    cnNorthwind.Open();

    string sql = "SELECT UnitsInStock FROM Products WHERE ProductID = @ProdID";

    using (var cmProducts = new NpgsqlCommand(sql, cnNorthwind))
    {
        cmProducts.Parameters.Add(new NpgsqlParameter("ProdID", NpgsqlTypes.NpgsqlDbType.Integer) { Value = 42 });

        int qty = Convert.ToInt32(cmProducts.ExecuteScalar());

        MessageBox.Show("Quantity in stock: " + qty.ToString());
    }
}





 


// Code for Retrieving Output and Return Values


/* Stored procedure with an input parameter named @CatID,
   an output parameter named @CatName, and a return value */

CREATE PROCEDURE dbo.CountProductsInCategory
  (
    @CatID int,
    @CatName nvarchar(15) OUTPUT
  )
AS
  DECLARE @ProdCount int
	
  SELECT @CatName = Categories.CategoryName, 
         @ProdCount = COUNT(Products.ProductID)
  FROM Categories INNER JOIN Products 
    ON Categories.CategoryID = Products.CategoryID
  WHERE (Categories.CategoryID = @CatID)
  GROUP BY Categories.CategoryName
	
  RETURN @ProdCount


// Set input parameters, execute the stored procedure, then 
// retrieve the output parameter and the return value


cmd.Parameters["@CatID"].Value = 1;
cn.Open();
cmd.ExecuteScalar(); // could use any ExecuteX method
cn.Close();

MessageBox.Show("Category name: " +
                cmd.Parameters["@CatName"].Value +
                "Number of products in category: " +
                cmd.Parameters["@RETURN_VALUE"].Value);


// в PostgreSQL не используется RETURN_VALUE как в SQL Server

// как это правильно адаптировать для PostgreSQL

// нужно создать функцию с OUT параметрами

CREATE OR REPLACE FUNCTION CountProductsInCategory(
    CatID integer,
    OUT CatName varchar(15),
    OUT ProdCount integer
) AS $$
BEGIN
    SELECT Categories.CategoryName, 
           COUNT(Products.ProductID)
    INTO CatName, ProdCount
    FROM Categories
    INNER JOIN Products 
        ON Categories.CategoryID = Products.CategoryID
    WHERE Categories.CategoryID = CatID
    GROUP BY Categories.CategoryName;
END;
$$ LANGUAGE plpgsql;

// вызвать эту функцию из C# 

using (var cn = new NpgsqlConnection("Host=localhost;Port=5432;Username=sa;Password=me2I81sour2;Database=Northwind;"))
{
    cn.Open();

    using (var cmd = new NpgsqlCommand("CountProductsInCategory", cn))
    {
        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        // Входной параметр
        cmd.Parameters.Add(new NpgsqlParameter("CatID", NpgsqlTypes.NpgsqlDbType.Integer) { Value = 1 });

        // Выходной параметр для имени категории
        var catNameParam = new NpgsqlParameter("CatName", NpgsqlTypes.NpgsqlDbType.Varchar, 15)
        {
            Direction = System.Data.ParameterDirection.Output
        };
        cmd.Parameters.Add(catNameParam);

        // Выходной параметр для количества продуктов
        var prodCountParam = new NpgsqlParameter("ProdCount", NpgsqlTypes.NpgsqlDbType.Integer)
        {
            Direction = System.Data.ParameterDirection.Output
        };
        cmd.Parameters.Add(prodCountParam);

        cmd.ExecuteNonQuery();

        string categoryName = (string)catNameParam.Value;
        int productCount = (int)prodCountParam.Value;

        MessageBox.Show("Category name: " + categoryName +
                        "\nNumber of products in category: " + productCount.ToString());
    }
}










// Code For Using a DataReader Object to Process a Result Set


NpgsqlCommand  cmdProducts  = new NpgsqlCommand( 
	"SELECT ProductName, UnitsInStock " +
	"FROM Products", cnNorthwind);

cnNorthwind.Open();

NpgsqlDataReader rdrProducts;

rdrProducts = cmdProducts.ExecuteReader(CommandBehavior.CloseConnection);

while (rdrProducts.Read())
{
  listBox1.Items.Add(rdrProducts.GetString(0) +
		"\t" + rdrProducts.GetInt16(1));
}
rdrProducts.Close();


// получить данные из таблицы базы данных PostgreSQL и обработать каждую строку результата

using (var cnNorthwind = new NpgsqlConnection("Host=localhost;Port=5432;Username=sa;Password=me2I81sour2;Database=Northwind;"))
{
    cnNorthwind.Open();

    using (var cmdProducts = new NpgsqlCommand(
        "SELECT \"ProductName\", \"UnitsInStock\" FROM \"Products\"", cnNorthwind))

    using (var rdrProducts = cmdProducts.ExecuteReader())
    {
        while (rdrProducts.Read())
        {
            string productName = rdrProducts.GetString(0);
            short unitsInStock = rdrProducts.GetInt16(1);

            listBox1.Items.Add($"{productName}\t{unitsInStock}");
        }
    }
}










// Code For Using a DataReader Object to Process a Multiple Result Sets


NpgsqlCommand cmdProducts  = new NpgsqlCommand( 
	"dbo.IncreasePrices", cnNorthwind);

cmdProducts.CommandType = CommandType.StoredProcedure;

NpgsqlDataReader rdrProducts; 

cnNorthwind.Open();

rdrProducts = cmdProducts.ExecuteReader(CommandBehavior.CloseConnection);

while (rdrProducts.Read())
{
  lstProducts.Items.Add(rdrProducts.GetString(0) +
		"\t" + rdrProducts.GetSqlMoney(1).ToDouble());
}

rdrProducts.NextResult();

while (rdrProducts.Read())
{
  lstDiscontinued.Items.Add(rdrProducts.GetString(0) +
		"\t" + rdrProducts.GetSqlMoney(1).ToDouble());
}


MessageBox.Show("Products affected: " +
	rdrProducts.RecordsAffected);

rdrProducts.Close();
 
// в PostgreSQL нужно специально проектировать функцию или процедуру с использованием REFCURSOR, чтобы вернуть несколько наборов результатов
// стандартная процедура с двумя SELECT не вернёт автоматически два набора, как в SQL Server
// в PostgreSQL с Npgsql почти всегда CommandType.Text, а вызовы процедур через CALL

using (var cnNorthwind = new NpgsqlConnection("Host=localhost;Port=5432;Username=sa;Password=me2I81sour2;Database=Northwind;"))
{
    cnNorthwind.Open();

    using (var cmdProducts = new NpgsqlCommand("CALL \"IncreasePrices\"()", cnNorthwind))

    using (var rdrProducts = cmdProducts.ExecuteReader())
    {
        while (rdrProducts.Read())
        {
            string productName = rdrProducts.GetString(0);
            decimal unitPrice = rdrProducts.GetDecimal(1);

            lstProducts.Items.Add($"{productName}\t{unitPrice}");
        }

        if (rdrProducts.NextResult())
        {
            while (rdrProducts.Read())
            {
                string productName = rdrProducts.GetString(0);
                decimal unitPrice = rdrProducts.GetDecimal(1);

                lstDiscontinued.Items.Add($"{productName}\t{unitPrice}");
            }
        }

        MessageBox.Show("Products affected: " + rdrProducts.RecordsAffected);
    }
}


// более реальный пример

CREATE OR REPLACE PROCEDURE "IncreasePrices"(
    INOUT products_cursor REFCURSOR,
    INOUT discontinued_cursor REFCURSOR
)
LANGUAGE plpgsql
AS $$
BEGIN
    -- Первый курсор: активные товары с обновлёнными ценами
    OPEN products_cursor FOR
        SELECT "ProductName", "UnitPrice" * 1.1 AS "NewUnitPrice"
        FROM "Products"
        WHERE "Discontinued" = FALSE;

    -- Второй курсор: товары, снятые с производства
    OPEN discontinued_cursor FOR
        SELECT "ProductName", "UnitPrice"
        FROM "Products"
        WHERE "Discontinued" = TRUE;
END;
$$;

// получить независимые наборы строк, возвращаемые одной процедурой, и обработать их последовательно
// в PostgreSQL курсоры работают в рамках одной транзакции, поэтому её нужно явно начать

using (var cnNorthwind = new NpgsqlConnection("Host=localhost;Port=5432;Username=sa;Password=me2I81sour2;Database=Northwind;"))
{
    cnNorthwind.Open();

    using (var transaction = cnNorthwind.BeginTransaction())
    {
        using (var cmd = new NpgsqlCommand("CALL \"IncreasePrices\"(@products_cursor, @discontinued_cursor);", cnNorthwind, transaction))
        {
            cmd.Parameters.Add(new NpgsqlParameter("products_cursor", NpgsqlTypes.NpgsqlDbType.Refcursor) { Value = "products_cursor" });
            cmd.Parameters.Add(new NpgsqlParameter("discontinued_cursor", NpgsqlTypes.NpgsqlDbType.Refcursor) { Value = "discontinued_cursor" });

            cmd.ExecuteNonQuery();
        }

        using (var cmdFetchProducts = new NpgsqlCommand("FETCH ALL FROM products_cursor;", cnNorthwind, transaction))
        using (var rdrProducts = cmdFetchProducts.ExecuteReader())
        {
            while (rdrProducts.Read())
            {
                string productName = rdrProducts.GetString(0);
                decimal newUnitPrice = rdrProducts.GetDecimal(1);

                lstProducts.Items.Add($"{productName}\t{newUnitPrice}");
            }
        }

        using (var cmdFetchDiscontinued = new NpgsqlCommand("FETCH ALL FROM discontinued_cursor;", cnNorthwind, transaction))
        using (var rdrDiscontinued = cmdFetchDiscontinued.ExecuteReader())
        {
            while (rdrDiscontinued.Read())
            {
                string productName = rdrDiscontinued.GetString(0);
                decimal unitPrice = rdrDiscontinued.GetDecimal(1);

                lstDiscontinued.Items.Add($"{productName}\t{unitPrice}");
            }
        }

        transaction.Commit();
    }
}

// проще через несколько отдельных запросов









// Code for Executing a Data Definition Language Command

NpgsqlCommand cmSummarizeProducts = new NpgsqlCommand( 
	"dbo.SummarizeProducts", cnNorthwind);

cmSummarizeProducts.CommandType = CommandType.StoredProcedure;

cnNorthwind.Open();

int affected = cmSummarizeProducts.ExecuteNonQuery();

cnNorthwind.Close();

MessageBox.Show("Rows affected: " + affected);

// SQL-команда DDL создания таблицы для PostgreSQL

CREATE TABLE IF NOT EXISTS "ProductSummary" (
    "ProductName" VARCHAR(40),
    "CategoryName" VARCHAR(15)
);

// двойные экранированные кавычки

using (var cnNorthwind = new NpgsqlConnection("Host=localhost;Port=5432;Username=sa;Password=me2I81sour2;Database=Northwind;"))
{
    cnNorthwind.Open();

    string ddl = @"CREATE TABLE IF NOT EXISTS ""ProductSummary"" (
                       ""ProductName"" VARCHAR(40),
                       ""CategoryName"" VARCHAR(15)
                   );";

    using (var cmSummarizeProducts = new NpgsqlCommand(ddl, cnNorthwind))
    {
        cmSummarizeProducts.CommandType = CommandType.Text;

        int affected = cmSummarizeProducts.ExecuteNonQuery();

        MessageBox.Show("DDL executed. Rows affected: " + affected);
    }
}

// процедура создания таблицы для PostgreSQL

CREATE OR REPLACE PROCEDURE "SummarizeProducts"()
LANGUAGE plpgsql
AS $$
BEGIN
    CREATE TABLE IF NOT EXISTS "ProductSummary" (
        "ProductName" VARCHAR(40),
        "CategoryName" VARCHAR(15)
    );
END;
$$;

using (var cnNorthwind = new NpgsqlConnection("Host=localhost;Port=5432;Username=sa;Password=me2I81sour2;Database=Northwind;"))
{
    cnNorthwind.Open();

    using (var cmSummarizeProducts = new NpgsqlCommand("CALL \"SummarizeProducts\"();", cnNorthwind))
    {
        cmSummarizeProducts.CommandType = CommandType.Text;

        int affected = cmSummarizeProducts.ExecuteNonQuery();

        MessageBox.Show("Procedure executed. Rows affected: " + affected);
    }
}










// Code for Executing a Data Manipulation Language Command

NpgsqlCommand cmd  = new NpgsqlCommand("dbo.IncreaseProductPrices", cnNorthwind);

cmd.CommandType = CommandType.StoredProcedure;

cnNorthwind.Open();

int affected = cmd.ExecuteNonQuery();

cnNorthwind.Close();

MessageBox.Show("Records affected: " + affected);

// процедура DML изменяет данные в таблицах для PostgreSQL
// ExecuteNonQuery() вернёт суммарное количество всех затронутых строк

CREATE OR REPLACE PROCEDURE "IncreaseProductPrices"()
LANGUAGE plpgsql
AS $$
BEGIN
    UPDATE "Products"
    SET "UnitPrice" = "UnitPrice" * 1.1
    WHERE "Discontinued" = FALSE;
END;
$$;

using (var cnNorthwind = new NpgsqlConnection("Host=localhost;Port=5432;Username=sa;Password=me2I81sour2;Database=Northwind;"))
{
    cnNorthwind.Open();

    using (var cmd = new NpgsqlCommand("CALL \"IncreaseProductPrices\"();", cnNorthwind))
    {
        cmd.CommandType = CommandType.Text;

        int affected = cmd.ExecuteNonQuery();

        MessageBox.Show("Records affected: " + affected);
    }
}






 



// Code for Performing a Transaction

// Open the database connection, and begin a transaction. 
// Execute two DELETE statements within the transaction. 
// Commit or rollback the transaction, as appropriate

cnNorthwind.Open();

NpgsqlTransaction trans = cnNorthwind.BeginTransaction();

NpgsqlCommand cmd = new NpgsqlCommand();
cmd.Connection = cnNorthwind;
cmd.Transaction = trans;

try
{
  cmd.CommandText = "DELETE [Order Details] WHERE ProductID = 42";

  cmd.ExecuteNonQuery();

  cmd.CommandText = "DELETE Products WHERE ProductID = 42";

  cmd.ExecuteNonQuery();

  trans.Commit();
}

catch (Exception e)
{
  trans.Rollback();
}
finally
{
  cn.Close();
}

// пример выполнения транзакции с двумя командами DELETE для PostgreSQL

using (var cnNorthwind = new NpgsqlConnection("Host=localhost;Port=5432;Username=sa;Password=me2I81sour2;Database=Northwind;"))
{
    cnNorthwind.Open();

    using (var trans = cnNorthwind.BeginTransaction())
    
    using (var cmd = new NpgsqlCommand())
    {
        cmd.Connection = cnNorthwind;
        cmd.Transaction = trans;

        try
        {
            cmd.CommandText = "DELETE FROM \"Order Details\" WHERE \"ProductID\" = 42;";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "DELETE FROM \"Products\" WHERE \"ProductID\" = 42;";
            cmd.ExecuteNonQuery();

            trans.Commit();
            MessageBox.Show("Transaction committed successfully.");
        }
        catch (Exception ex)
        {
            trans.Rollback();
            MessageBox.Show("Transaction rolled back. Error: " + ex.Message);
        }
    }
}










// How to Create a Foreign Key Constraint

dtCustomers = dsNorthwind.Tables["Customers"];
dtOrders = dsNorthwind.Tables["Orders"];

ForeignKeyConstraint fkcCustomersOrders = 
  new ForeignKeyConstraint("FK_CustomersOrders",
  dtCustomers.Columns["CustomerID"], 
  dtOrders.Columns["CustomerID"]);

fkcCustomersOrders.DeleteRule = Rule.None;

dtOrders.Constraints.Add(fkcCustomersOrders);


// C#-код, если нужно установить связь ForeignKeyConstraint в DataSet

using System;
using System.Data;

DataSet dsNorthwind = new DataSet();

// Создаём таблицы Customers и Orders

DataTable dtCustomers = new DataTable("Customers");
dtCustomers.Columns.Add("CustomerID", typeof(int));
dtCustomers.PrimaryKey = new DataColumn[] { dtCustomers.Columns["CustomerID"] };

DataTable dtOrders = new DataTable("Orders");
dtOrders.Columns.Add("OrderID", typeof(int));
dtOrders.Columns.Add("CustomerID", typeof(int));

dsNorthwind.Tables.Add(dtCustomers);
dsNorthwind.Tables.Add(dtOrders);

// Создаём ограничение внешнего ключа

ForeignKeyConstraint fkcCustomersOrders = new ForeignKeyConstraint(
    "FK_CustomersOrders",
    dtCustomers.Columns["CustomerID"],
    dtOrders.Columns["CustomerID"]
);

fkcCustomersOrders.DeleteRule = Rule.None; // при удалении родительской записи зависимые записи не будут удалены и выбросится исключение 
dtOrders.Constraints.Add(fkcCustomersOrders);

// Вывод информации о связи
Console.WriteLine("Foreign Key Constraint added: " + fkcCustomersOrders.ConstraintName);










// How to Position on a Record 

private CurrencyManager cmCustomers;

private void Form1_Load(object sender, System.EventArgs e) 
{
	txtCompanyName.DataBindings.Add( 
		"Text", dtCustomers, "CompanyName");
	cmCustomers = (CurrencyManager)(this.BindingContext[dtCustomers]);
	cmCustomers.Position = 0;
}

private void btnMoveNext(object sender, System.EventArgs e)
{	if (cmCustomers.Position != cmCustomers.Count - 1)
		{
			cmCustomers.Position += 1;
		}
}

private void btnMoveFirst(object sender, System.EventArgs e)
{
   cmCustomers.Position = 0;
}

private void btnMovePrevious(object sender, System.EventArgs e)
{   if (cmCustomers.Position !=0) 
      {
        cmCustomers.Position -= 1;
      }	
}

private void btnMoveLast(object sender, System.EventArgs e)
{
   cmCustomers.Position = cmCustomers.Count - 1;
}


// управление записями с CurrencyManager в приложении Windows Forms 

using System;
using System.Data;
using System.Windows.Forms;

public partial class Form1 : Form
{
    private CurrencyManager cmCustomers;
    private DataTable dtCustomers;

    public Form1()
    {
        InitializeComponent();
        LoadData();
    }

    private void LoadData()
    {
        // Создаём DataTable
        dtCustomers = new DataTable();
        dtCustomers.Columns.Add("CustomerID", typeof(int));
        dtCustomers.Columns.Add("CompanyName", typeof(string));

        // Добавляем тестовые данные
        dtCustomers.Rows.Add(1, "Company A");
        dtCustomers.Rows.Add(2, "Company B");
        dtCustomers.Rows.Add(3, "Company C");

        // Привязка данных к TextBox
        txtCompanyName.DataBindings.Add("Text", dtCustomers, "CompanyName");

        // Устанавливаем CurrencyManager
        cmCustomers = (CurrencyManager)BindingContext[dtCustomers];
        cmCustomers.Position = 0;
    }

    private void btnMoveNext_Click(object sender, EventArgs e)
    {
        if (cmCustomers.Position < cmCustomers.Count - 1)
        {
            cmCustomers.Position += 1;
        }
    }

    private void btnMoveFirst_Click(object sender, EventArgs e)
    {
        cmCustomers.Position = 0;
    }

    private void btnMovePrevious_Click(object sender, EventArgs e)
    {
        if (cmCustomers.Position > 0)
        {
            cmCustomers.Position -= 1;
        }
    }

    private void btnMoveLast_Click(object sender, EventArgs e)
    {
        cmCustomers.Position = cmCustomers.Count - 1;
    }
}









// Two-Level XSD Schema


<?xml version="1.0" standalone="yes"?>
<xsd:schema id="PersonPet" targetNamespace="" xmlns="" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xsd:element name="PersonPet" msdata:IsDataSet="true">
    <xsd:complexType>
      <xsd:choice maxOccurs="unbounded">
        <xsd:element name="Person">
          <xsd:complexType>
            <xsd:sequence>
              <xsd:element name="ID" msdata:AutoIncrement="true" type="xsd:int" />
              <xsd:element name="Name" type="xsd:string" minOccurs="0" />
              <xsd:element name="Age" type="xsd:int" minOccurs="0" />
            </xsd:sequence>
          </xsd:complexType>
        </xsd:element>
        <xsd:element name="Pet">
          <xsd:complexType>
            <xsd:sequence>
              <xsd:element name="ID" msdata:AutoIncrement="true" type="xsd:int" />
              <xsd:element name="OwnerID" type="xsd:int" minOccurs="0" />
              <xsd:element name="Name" type="xsd:string" minOccurs="0" />
              <xsd:element name="Type" type="xsd:string" minOccurs="0" />
            </xsd:sequence>
          </xsd:complexType>
        </xsd:element>
      </xsd:choice>
    </xsd:complexType>
    <xsd:unique name="Constraint1" msdata:PrimaryKey="true">
      <xsd:selector xpath=".//Person" />
      <xsd:field xpath="ID" />
    </xsd:unique>
    <xsd:unique name="Pet_Constraint1" msdata:ConstraintName="Constraint1" msdata:PrimaryKey="true">
      <xsd:selector xpath=".//Pet" />
      <xsd:field xpath="ID" />
    </xsd:unique>
  </xsd:element>
  <xsd:annotation>
    <xsd:appinfo>
      <msdata:Relationship name="PersonPet" msdata:parent="Person" msdata:child="Pet" msdata:parentkey="ID" msdata:childkey="OwnerID" />
    </xsd:appinfo>
  </xsd:annotation>
</xsd:schema>


// XML Data Document based on Schema (Code)


<PersonPet xmlns = "http://tempuri/PersonPet.xsd">
  <Person>
    <ID>0</ID>
    <Name>Mark</Name>
    <Age>18</Age>
  </Person>
  <Person>
    <ID>1</ID>
    <Name>William</Name>
    <Age>12</Age>
  </Person>
  <Person>
    <ID>2</ID>
    <Name>James</Name>
    <Age>7</Age>
  </Person>
  <Person>
    <ID>3</ID>
    <Name>Levi</Name>
    <Age>4</Age>
  </Person>
  <Pet>
    <ID>0</ID>
    <OwnerID>0</OwnerID>
    <Name>Frank</Name>
    <Type>cat</Type>
  </Pet>
  <Pet>
    <ID>1</ID>
    <OwnerID>1</OwnerID>
    <Name>Rex</Name>
    <Type>dog</Type>
  </Pet>
  <Pet>
    <ID>2</ID>
    <OwnerID>2</OwnerID>
    <Name>Cottontail</Name>
    <Type>rabbit</Type>
  </Pet>
  <Pet>
    <ID>3</ID>
    <OwnerID>3</OwnerID>
    <Name>Sid</Name>
    <Type>snake</Type>
  </Pet>
  <Pet>
    <ID>4</ID>
    <OwnerID>3</OwnerID>
    <Name>Tickles</Name>
    <Type>spider</Type>
  </Pet>
  <Pet>
    <ID>5</ID>
    <OwnerID>1</OwnerID>
    <Name>Tweetie</Name>
    <Type>canary</Type>
  </Pet>
</PersonPet>


// Использование схемы XSD и DataSet в .NET

// Загрузка XSD-схемы
DataSet ds = new DataSet();
ds.ReadXmlSchema("PersonPet.xsd");

// Загрузка XML-данных
ds.ReadXml("PersonPet.xml");

// Вывод данных с защитой от NullReferenceException

if (ds.Tables.Contains("Person"))
{
    foreach (DataRow row in ds.Tables["Person"].Rows)
    {
        Console.WriteLine($"Человек: {row["Name"]}, Возраст: {row["Age"]}");
    }
}
else
{
    Console.WriteLine("Ошибка: Таблица Person не найдена.");
}

if (ds.Tables.Contains("Pet"))
{
    foreach (DataRow row in ds.Tables["Pet"].Rows)
    {
        string ownerID = row.IsNull("OwnerID") ? "нет владельца" : row["OwnerID"].ToString();

        Console.WriteLine($"Питомец: {row["Name"]}, Тип: {row["Type"]}, Владелец ID: {ownerID}");
    }
}
else
{
    Console.WriteLine("Ошибка: Таблица Pet не найдена.");
}








// XSD schema that defines the parts of a relational table named “Orders”

// XSD-схема отражает реляционную структуру данных

<!-- The element name followed by a complexType defines the “Orders” table -->

<xsd:element name="Orders" minOccurs="0" maxOccurs="unbounded">
   
   <xsd:complexType>
      <xsd:sequence>
         
         <xsd:element name="OrderID" type="xsd:int"/>
         <!--This next block defines the OrderDetails table -->
         
         <xsd:element name="OrderDetails" minOccurs="0" maxOccurs="unbounded">
            <xsd:complexType>
               <xsd:sequence>
                  <xsd:element name="ProductID" type="xsd:int"/>
                  <xsd:element name="UnitPrice" type="xsd:number"/>
                  <xsd:element name="Quantity"  type="xsd:short"/>
               </xsd:sequence>
            </xsd:complexType>
         </xsd:element>

         <xsd:element name="OrderDate" type="xsd:dateTime" minOccurs="0"/>
      </xsd:sequence>
      
      <xsd:attribute name="CustomerID" type="xsd:string" use="prohibited" />
   </xsd:complexType>

    <!-- Each OrderID is unique -->
    <xsd:unique name="Orders_Constraint">       
       <xsd:selector xpath=".//Orders" />
       <xsd:field xpath="OrderID" />
    </xsd:unique>
    
    <!-- Primary key -->
    <xsd:key name="OrderDetails_Constraint">    
       <xsd:selector xpath=".//OrderDetails" />
       <xsd:field xpath="OrderID" />
       <xsd:field xpath="ProductID" />
    </xsd:key>

</xsd:element>











// Loading an XSD Schema from a File into a Dataset


private const string PurchaseSchema = 
                                @"C:\sampledata\Purchase.xsd";
private DataSet myDS; 

private void Load_XSD()
{
	try
	{	
		myDS = new DataSet();

		Console.WriteLine ("Reading the Schema file");
		myDS.ReadXmlSchema(PurchaseSchema);
	}
	catch(Exception e)
	{	
		Console.WriteLine("Exception: " + e.ToString());
	}
}


// Код подготовленый к работе с файлами XSD (если некорректные типы данных XSD)

using System;
using System.Data;
using System.IO;

private const string PurchaseSchema = @"C:\sampledata\Purchase.xsd";
private DataSet myDS;

private void Load_XSD()
{
    try
    {
        if (!File.Exists(PurchaseSchema))
        {
            Console.WriteLine("Ошибка: Файл XSD не найден.");
            return;
        }

        myDS = new DataSet();

        Console.WriteLine("Читаем XSD-схему: " + PurchaseSchema);
        myDS.ReadXmlSchema(PurchaseSchema);
        Console.WriteLine("Схема загружена успешно.");
    }
    catch (Exception e)
    {
        Console.WriteLine("Ошибка загрузки XSD: " + e.Message);
    }
}










// Loading an XSD into a DataSet by Using a Stream Object 

private const string PurchaseSchema =  
                                @"C:\sampledata\Purchase.xsd";
private DataSet myDS;

private void Load_XSD()
{
	StreamReader myStreamReader = null; 

	try
	{
		myStreamReader = new StreamReader(PurchaseSchema);

		myDS = new DataSet();

		Console.WriteLine ("Reading the Schema file");
		myDS.ReadXmlSchema(myStreamReader);
	}
	catch (Exception e)
	{	
		Console.WriteLine("Exception: " + e.ToString());
	}
	finally
	{	
		if (myStreamReader!=null)
			myStreamReader.Close();
	
	}
}


// код безопасно загружает XSD-схему из потока (из памяти, сети или ресурса)

using System;
using System.Data;
using System.IO;

string schemaContent = @"
<?xml version='1.0' encoding='utf-8'?> 
<xsdschema>
<!-- XSD -->
</xsd:schema>
";
string filePath = @"C:\sampledata\Purchase.xsd";

//пример только для открытия потока (StreamReader)
Directory.CreateDirectory(Path.GetDirectoryName(filePath));
File.WriteAllText(filePath, schemaContent);

private const string PurchaseSchema = @"C:\sampledata\Purchase.xsd"; 
private DataSet myDS;

private void Load_XSD()
{
    if (!File.Exists(PurchaseSchema))
    {
        Console.WriteLine("Ошибка: Файл XSD не найден.");
        return;
    }

    try
    {
        using (StreamReader myStreamReader = new StreamReader(PurchaseSchema))
        {
            myDS = new DataSet();
            Console.WriteLine("Читаем XSD-схему из потока");
            myDS.ReadXmlSchema(myStreamReader);
            Console.WriteLine("Схема загружена успешно.");
        }
    }
    catch (Exception e)
    {
        Console.WriteLine("Ошибка загрузки XSD: " + e.Message);
    }
}











// Printing out the Structure of a DataSet named myDS 

private void DisplayTableStructure()
{
  Console.WriteLine("Table structure");

  //Print the number of tables 
  Console.WriteLine("Tables count=" + myDS.Tables.Count.ToString());

  //Print the table and column names

  for (int i = 0; i < myDS.Tables.Count; i++)
    {
	//Print the table names
	Console.WriteLine("TableName='" + myDS.Tables[i].TableName + "'.");
	Console.WriteLine("Columns count=" + myDS.Tables[i].Columns.Count.ToString());

  	for (int j = 0; j < myDS.Tables[i].Columns.Count; j++)
	{      
		//Print the column names and data types    
		Console.WriteLine( "\t" +
		        " ColumnName='" + myDS.Tables[i].Columns[j].ColumnName +
		        " DataType='"   + myDS.Tables[i].Columns[j].DataType.ToString() );
    	}
    	Console.WriteLine()
   }
}


// код выводит структуру DataSet в консоль

private void DisplayTableStructure()
{
    if (myDS == null || myDS.Tables.Count == 0)
    {
        Console.WriteLine("DataSet пуст или не загружен.");
        return;
    }

    Console.WriteLine("Table structure");

    // Вывод количества таблиц
    Console.WriteLine($"Tables count={myDS.Tables.Count}");

    foreach (DataTable table in myDS.Tables)
    {
        Console.WriteLine($"TableName='{table.TableName}', Columns count={table.Columns.Count}");

        foreach (DataColumn column in table.Columns)
        {
            Console.WriteLine($"\tColumnName='{column.ColumnName}', DataType='{column.DataType}'");
        }

        Console.WriteLine();
    }
}










