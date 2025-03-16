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










// Loading a Schema and Data into a New DataSet 

private const string PurchaseSchema = 
                                @"C:\sampledata\Purchase.xsd";

private void ReadXmlDataOnly()
{
	try
	{
		myDS = new DataSet();
		Console.WriteLine("Reading the Schema file");
		myDS.ReadXmlSchema(PurchaseSchema); 
		
		Console.WriteLine("Loading the XML data file");
		myDS.ReadXml(@"C:\sampledata\PurchaseData.xml",  
                  XmlReadMode.IgnoreSchema);  

		dataGrid1.DataSource = myDS.Tables[1];  
	}

	catch(Exception e)
	{
		Console.WriteLine("Exception: " + e.ToString());
	}
}

// код загружает XSD-схему и XML-данные в DataSet и привязывает таблицу данных к элементу управления dataGrid1

using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

private const string PurchaseSchema = @"C:\sampledata\Purchase.xsd";
private const string PurchaseData = @"C:\sampledata\PurchaseData.xml";
private DataSet myDS;

private void ReadXmlDataOnly()
{
    try
    {
        if (!File.Exists(PurchaseSchema) || !File.Exists(PurchaseData))
        {
            Console.WriteLine("Ошибка: XSD или XML файл не найден.");
            return;
        }

        myDS = new DataSet();

        Console.WriteLine("Читаем XSD-схему...");
        myDS.ReadXmlSchema(PurchaseSchema);

        Console.WriteLine("Загружаем XML-данные...");
        myDS.ReadXml(PurchaseData, XmlReadMode.IgnoreSchema);

        Console.WriteLine($"Загружено таблиц: {myDS.Tables.Count}");
        foreach (DataTable table in myDS.Tables)
        {
            Console.WriteLine($"Таблица '{table.TableName}' содержит {table.Rows.Count} записей.");
        }

        if (myDS.Tables.Count > 0)
        {
            dataGrid1.DataSource = myDS.Tables[0]; // Привязываем первую таблицу
        }
        else
        {
            Console.WriteLine("Ошибка: В DataSet нет загруженных таблиц.");
        }
    }
    catch (Exception e)
    {
        Console.WriteLine("Ошибка загрузки данных: " + e.Message);
    }
}












// Reading Inline Schema and XML Data 


private void ReadXmlDataAndSchema()
{
	try
	{
		myDS = new DataSet();

		myDS.ReadXml(@"C:\sampledata\PurchaseOrder.xml", 
                   XmlReadMode.ReadSchema);
	}

	catch (Exception e)
	{
		Console.WriteLine("Exception: " + e.ToString());
	}
	
}


// код загружает XSD-схему и XML-данные одновременно

using System;
using System.Data;
using System.IO;

private void ReadXmlDataAndSchema()
{
    string xmlFile = @"C:\sampledata\PurchaseOrder.xml";

    try
    {
        if (!File.Exists(xmlFile))
        {
            Console.WriteLine("Ошибка: XML файл не найден.");
            return;
        }

        myDS = new DataSet();

        Console.WriteLine("Читаем XML-файл со встроенной схемой...");
        myDS.ReadXml(xmlFile, XmlReadMode.ReadSchema);

        if (myDS.Tables.Count == 0)
        {
            Console.WriteLine("Ошибка: XML не содержит встроенной схемы.");
            return;
        }

        Console.WriteLine($"Загружено таблиц: {myDS.Tables.Count}");
        foreach (DataTable table in myDS.Tables)
        {
            Console.WriteLine($"Таблица: {table.TableName}, Количество записей: {table.Rows.Count}");
        }
    }
    catch (Exception e)
    {
        Console.WriteLine("Ошибка загрузки XML: " + e.Message);
    }
}










// Inferring a Schema from XML Data 


private void ReadXmlDataInferSchema()
{
	try
	{
		myDS = new DataSet();

		myDS.ReadXml(@"C:\sampledata\PurchaseOrder.xml", 
                   XmlReadMode.InferSchema);

	catch (Exception e)
	{
		Console.WriteLine("Exception: " + e.ToString());
	}
}


// код загружает XML-данные в DataSet, автоматически определяя схему на основе первых вхождений данных

using System;
using System.Data;
using System.IO;

private void ReadXmlDataInferSchema()
{
    string xmlFile = @"C:\sampledata\PurchaseOrder.xml";

    try
    {
        if (!File.Exists(xmlFile))
        {
            Console.WriteLine("Ошибка: XML файл не найден.");
            return;
        }

        myDS = new DataSet();

        Console.WriteLine("Читаем XML-файл с автоматическим определением схемы...");
        myDS.ReadXml(xmlFile, XmlReadMode.InferSchema);

        if (myDS.Tables.Count == 0)
        {
            Console.WriteLine("Ошибка: XML не содержит данных, схема не определена.");
            return;
        }

        Console.WriteLine($"Загружено таблиц: {myDS.Tables.Count}");
        foreach (DataTable table in myDS.Tables)
        {
            Console.WriteLine($"Таблица: {table.TableName}, Количество записей: {table.Rows.Count}");
        }
    }
    catch (Exception e)
    {
        Console.WriteLine("Ошибка загрузки XML: " + e.Message);
    }
}











// Loading a DataSet by Using WriteXMLSchema and Data from an XML File


private void SaveXSDSchema()
{
	try
	{
		myDS = new DataSet();

		//Load an inline schema and data from an XML file
		myDS.ReadXml(@"C:\sampledata\PurchaseOrder.xml", 
			XmlReadMode.ReadSchema);

		//Save the schema to an XSD file
		myDS.WriteXmlSchema(@"C:\sampledata\POSchema.xsd");
	}

	catch (Exception e)
	{
		Console.WriteLine("Exception: "+ e.ToString());
	}
}


// код загружает XML-данные и встроенную схему в DataSet и затем сохраняет XSD-схему в файл

using System;
using System.Data;
using System.IO;

private void SaveXSDSchema()
{
    string xmlFile = @"C:\sampledata\PurchaseOrder.xml";
    string schemaFile = @"C:\sampledata\POSchema.xsd";

    try
    {
        if (!File.Exists(xmlFile))
        {
            Console.WriteLine("Ошибка: XML-файл не найден.");
            return;
        }

        myDS = new DataSet();

        Console.WriteLine("Читаем XML-файл со встроенной схемой...");
        myDS.ReadXml(xmlFile, XmlReadMode.ReadSchema);

        if (myDS.Tables.Count == 0)
        {
            Console.WriteLine("Ошибка: XML не содержит схемы, XSD не сохранён.");
            return;
        }

        Directory.CreateDirectory(Path.GetDirectoryName(schemaFile));

        Console.WriteLine($"Сохраняем XSD-схему в {schemaFile}");
        myDS.WriteXmlSchema(schemaFile);
        Console.WriteLine("Схема успешно сохранена.");

        Console.WriteLine($"Загружено таблиц: {myDS.Tables.Count}");
        foreach (DataTable table in myDS.Tables)
        {
            Console.WriteLine($"Таблица: {table.TableName}, Количество записей: {table.Rows.Count}");
        }
    }
    catch (Exception e)
    {
        Console.WriteLine("Ошибка: " + e.Message);
    }
}









// Using the GetXmlSchema Method of the DataSet object


private void XSDSchemaToString()
{
	try
	{
		string StrPurchaseSchema;
		myDS = new DataSet();

		//Load an inline schema and data from an XML file
		myDS.ReadXml(@"C:\sampledata\PurchaseOrder.xml", 
			XmlReadMode.ReadSchema);

		//Get the schema from the DataSet and load it 
		//into a string
		StrPurchaseSchema = myDS.GetXmlSchema();

	catch (Exception e)
	{
		Console.WriteLine("Exception: " + e.ToString());
	}
}


// код загружает XML-данные со встроенной XSD-схемой в DataSet и извлекает XSD-схему 

using System;
using System.Data;
using System.IO;

private void XSDSchemaToString()
{
    string xmlFile = @"C:\sampledata\PurchaseOrder.xml";

    try
    {
        if (!File.Exists(xmlFile))
        {
            Console.WriteLine("Ошибка: XML-файл не найден.");
            return;
        }

        myDS = new DataSet();

        Console.WriteLine("Читаем XML-файл со встроенной схемой...");
        myDS.ReadXml(xmlFile, XmlReadMode.ReadSchema);

        if (myDS.Tables.Count == 0)
        {
            Console.WriteLine("Ошибка: XML не содержит схемы.");
            return;
        }

        string StrPurchaseSchema = myDS.GetXmlSchema();

        Console.WriteLine("Извлечённая XSD-схема:");
        Console.WriteLine(StrPurchaseSchema);
    }
    catch (Exception e)
    {
        Console.WriteLine("Ошибка: " + e.Message);
    }
}










// Writing XML Data to a File 


private void SaveXMLDataOnly()
{
	try
	{
		string StrPurchaseSchema;
		myDS = new DataSet();

		//Load an inline schema and data from an XML file
		myDS.ReadXml(@"C:\sampledata\PurchaseOrder.xml", 
			XmlReadMode.ReadSchema);
		
		//Save the data portion of the DataSet to a file 
		myDS.WriteXml(@"C:\sampledata\CurrentOrders.xml", 
			XmlWriteMode.IgnoreSchema);
	}
	
	catch (Exception e)
	{
		Console.WriteLine("Exception: " + e.ToString());
	}
}


// код загружает XML-данные со встроенной XSD-схемой в DataSet и сохраняет только данные в XML-файл

using System;
using System.Data;
using System.IO;

private void SaveXMLDataOnly()
{
    string inputFile = @"C:\sampledata\PurchaseOrder.xml";
    string outputFile = @"C:\sampledata\CurrentOrders.xml";

    try
    {
        if (!File.Exists(inputFile))
        {
            Console.WriteLine("Ошибка: XML-файл не найден.");
            return;
        }

        myDS = new DataSet();

        Console.WriteLine("Читаем XML-файл со встроенной схемой...");
        myDS.ReadXml(inputFile, XmlReadMode.ReadSchema);

        if (myDS.Tables.Count == 0 || myDS.Tables[0].Rows.Count == 0)
        {
            Console.WriteLine("Ошибка: XML не содержит данных, файл не сохранён.");
            return;
        }

        Directory.CreateDirectory(Path.GetDirectoryName(outputFile));

        Console.WriteLine($"Сохраняем XML-данные в {outputFile}");
        myDS.WriteXml(outputFile, XmlWriteMode.IgnoreSchema);
        Console.WriteLine("Данные успешно сохранены.");
    }
    catch (Exception e)
    {
        Console.WriteLine("Ошибка: " + e.Message);
    }
}










// Example of a DiffGram


<diffgr:diffgram 
	xmlns:msdata="urn:schemas-microsoft-com:xml-msdata" 
	xmlns:diffgr="urn:schemas-microsoft-com:xml-diffgram-v1">

  <CustomerDataSet>
    <Customers diffgr:id="Customers1"msdata:rowOrder="0" 
               diffgr:hasChanges="modified">
      <CustomerID>ALFKI</CustomerID>
      <CompanyName>New Company</CompanyName>
    </Customers>
    <Customers diffgr:id="Customers2" msdata:rowOrder="1">
      <CustomerID>ANATR</CustomerID>
      <CompanyName>Ana Trujillo Emparedados y helados</CompanyName>
    </Customers>
  </CustomerDataSet>

  <diffgr:before>
    <Customers diffgr:id="Customers1" msdata:rowOrder="0">
      <CustomerID>ALFKI</CustomerID>
      <CompanyName>Alfreds Futterkiste</CompanyName>
    </Customers>
  </diffgr:before>
</diffgr:diffgram>










// Creating a DiffGram


private void SaveDataSetChanges()
{
	try
	{
		string StrPurchaseSchema;
		myDS = new DataSet();

		//Load an inline schema and data from an XML file
		myDS.ReadXml(@"C:\sampledata\Customers.xml", 
			XmlReadMode.ReadSchema);

		//Make a change to information in the DataSet
		//Delete a row
		myDS.Tables[0].Rows[0].Remove();

		//Save the data portion of the DataSet as a Diffgram 
		myDS.WriteXml(@"C:\sampledata\CustomerChanges.xml", 
			XmlWriteMode.DiffGram);
	}

	catch (Exception e)
	{
		Console.WriteLine("Exception: " + e.ToString());
	}
}









//Creating a Data Adapter Programmatically


NpgsqlDataAdapter daProducts = new NpgsqlDataAdapter();

NpgsqlConnection cnNorthwind = new NpgsqlConnection(
  "Host=localhost;Database=Northwind;" +
  "Username=user;Password=password;");

NpgsqlCommand cmSelect = new NpgsqlCommand(
  "SELECT * FROM Products", cnNorthwind);

daProducts.SelectCommand = cmSelect;


// добавил DataTable и явное перечисление столбцов

private void LoadData()
{
    try
    {
        string connectionString = "Host=localhost;Port=5432;Database=Northwind;Username=user;Password=password;";
        
        using (var cnNorthwind = new NpgsqlConnection(connectionString))

        using (var cmSelect = new NpgsqlCommand("SELECT \"ProductID\", \"ProductName\", \"UnitPrice\" FROM \"Products\"", cnNorthwind))

        using (var daProducts = new NpgsqlDataAdapter(cmSelect))
        {
            DataTable dtProducts = new DataTable();
            daProducts.Fill(dtProducts);

            Console.WriteLine("Загружено строк: " + dtProducts.Rows.Count);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Ошибка загрузки данных: " + ex.Message);
    }
}










// Creating a DataAdapter that Uses Existing Stored Procedures


NpgsqlDataAdapter daProdCat = new NpgsqlDataAdapter();

NpgsqlCommand cmSelect = new NpgsqlCommand();
cmSelect.Connection = cnNorthwind;
cmSelect.CommandText = "GetProductsAndCategories";
cmSelect.CommandType = CommandType.StoredProcedure;

daProdCat.SelectCommand = cmSelect;











// Filling a DataSet by Using a DataAdapter


DataSet dsCustomers = new DataSet();
dsCustomers.Tables.Add(new DataTable("Customers"));

dsCustomers.Tables[0].BeginLoadData();
daCustomers.Fill(dsCustomers, "Customers");
dsCustomers.Tables[0].EndLoadData();

dataGrid1.DataSource = dsCustomers.Tables[0].DefaultView;


// Fill() создаёт таблицу автоматически + пробросили в DataDrid

private void LoadCustomers()
{
    string connectionString = "Host=localhost;Port=5432;Database=Northwind;Username=user;Password=password;";
    
    try
    {
        using (var cnNorthwind = new NpgsqlConnection(connectionString))

        using (var daCustomers = new NpgsqlDataAdapter("SELECT * FROM \"Customers\"", cnNorthwind))
        {
            DataSet dsCustomers = new DataSet();

            Console.WriteLine("Загружаем данные...");
            dsCustomers.BeginInit();

            daCustomers.Fill(dsCustomers, "Customers");

            dsCustomers.EndInit();

            if (dsCustomers.Tables.Count == 0 || dsCustomers.Tables["Customers"].Rows.Count == 0)
            {
                Console.WriteLine("Ошибка: Данные не загружены.");
                return;
            }

            dataGrid1.DataSource = dsCustomers.Tables["Customers"].DefaultView;

            Console.WriteLine($"Загружено строк: {dsCustomers.Tables["Customers"].Rows.Count}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Ошибка загрузки данных: " + ex.Message);
    }
}










// How to Infer Additional Constraints for a DataSet


//Using MissingSchemaAction
DataSet dsCustomers = new DataSet();
daCustomers.MissingSchemaAction =
                           MissingSchemaAction.AddWithKey;
daCustomers.Fill(dsCustomers);
dataGrid1.DataSource = dsCustomers.Tables[0].DefaultView;

// Using FillSchema
DataSet dsCustomers = new DataSet();
daCustomers.FillSchema(dsCustomers, SchemaType.Mapped);
daCustomers.Fill(dsCustomers);
dataGrid1.DataSource = dsCustomers.Tables[0].DefaultView;


// два способа автоматического создания ограничений

// MissingSchemaAction

DataSet dsCustomers = new DataSet();
daCustomers.MissingSchemaAction = MissingSchemaAction.AddWithKey;
daCustomers.Fill(dsCustomers);
dataGrid1.DataSource = dsCustomers.Tables[0].DefaultView;

// FillSchema заполняет схему таблицы, типы данных, ключи

DataSet dsCustomers = new DataSet();
daCustomers.FillSchema(dsCustomers, SchemaType.Mapped);
daCustomers.Fill(dsCustomers);
dataGrid1.DataSource = dsCustomers.Tables[0].DefaultView;








// Defining a DataSet Schema Programmatically


// Create the DataTable and DataColumns
DataTable table = new DataTable("Customers");
DataColumn c1 = new DataColumn("CustomerID",  typeof(String));
DataColumn c2 = new DataColumn("CompanyName", typeof(String));
DataColumn c3 = new DataColumn("ContactName", typeof(String));

// Add DataColumns and Constraints to the DataTable
table.Columns.Add(c1);
table.Columns.Add(c2);
table.Columns.Add(c3);
table.Constraints.Add("PK_CustomerID", c1, true);

// Create the DataSet, and add the DataTable to it
DataSet dsCustomers = new DataSet();
dsCustomers.Tables.Add(table);

// Fill DataSet by using a DataAdapter, and bind to a DataGrid
dsCustomers.Tables[0].BeginLoadData();
daCustomers.Fill(dsCustomers, "Customers");
dsCustomers.Tables[0].EndLoadData();
dataGrid1.DataSource = dsCustomers.Tables[0].DefaultView;


// оптимизированный пример сокращённый способ создания DataTable

private void LoadCustomers()
{
    string connectionString = "Host=localhost;Port=5432;Database=Northwind;Username=user;Password=password;";

    try
    {
        using (var cnNorthwind = new NpgsqlConnection(connectionString))
        using (var daCustomers = new NpgsqlDataAdapter("SELECT * FROM \"Customers\"", cnNorthwind))
        {
            DataSet dsCustomers = new DataSet();

            // Создаём DataTable с колонками и ограничением Primary Key
            DataTable table = new DataTable("Customers")
            {
                Columns =
                {
                    new DataColumn("CustomerID", typeof(String)),
                    new DataColumn("CompanyName", typeof(String)),
                    new DataColumn("ContactName", typeof(String))
                }
            };

            table.Constraints.Add("PK_CustomerID", table.Columns["CustomerID"], true);
            dsCustomers.Tables.Add(table);

            Console.WriteLine("Загружаем данные...");
            dsCustomers.Tables["Customers"].BeginLoadData();

            daCustomers.Fill(dsCustomers, "Customers");

            dsCustomers.Tables["Customers"].EndLoadData();

            if (dsCustomers.Tables["Customers"].Rows.Count == 0)
            {
                Console.WriteLine("Ошибка: Данные не загружены.");
                return;
            }

            dataGrid1.DataSource = dsCustomers.Tables["Customers"].DefaultView;
            Console.WriteLine($"Загружено строк: {dsCustomers.Tables["Customers"].Rows.Count}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Ошибка загрузки данных: " + ex.Message);
    }
}








// Using Current and Original Data in a Row 


foreach (DataRow row in this.dsCustomers.Customers.Rows)
{
  String msg = "";
  if (row.RowState == DataRowState.Added || 
      row.RowState == DataRowState.Unchanged)
  { 
    msg = "Current data:\n" +
      row["CompanyName", DataRowVersion.Current] + ", " +
      row["ContactName", DataRowVersion.Current];
  }
  else if (row.RowState == DataRowState.Deleted)
  {
    msg = "Original data:\n" +
      row["CompanyName", DataRowVersion.Original] + ", " +
      row["ContactName", DataRowVersion.Original];
  }
  else if (row.RowState == DataRowState.Modified)
  {
    msg = "Original data:\n" +
      row["CompanyName", DataRowVersion.Original] + ", " +
      row["ContactName", DataRowVersion.Original] + "\n";

    msg = msg + "Current data:\n" +
      row["CompanyName", DataRowVersion.Current] + ", " +
      row["ContactName", DataRowVersion.Current];
  }
  MessageBox.Show(msg, "RowState: " + row.RowState);
}


// как работать с текущими и оригинальными значениями в DataRow

// проверка RowStateException

private void ShowRowStates()
{
    foreach (DataRow row in this.dsCustomers.Customers.Rows)
    {
        string msg = "";

        if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Unchanged)
        {
            msg = "Current data:\n" +
                  row["CompanyName", DataRowVersion.Current] + ", " +
                  (row.IsNull("ContactName") ? "NULL" : row["ContactName", DataRowVersion.Current].ToString());
        }
        else if (row.RowState == DataRowState.Deleted)
        {
            if (row.HasVersion(DataRowVersion.Original))
            {
                msg = "Original data:\n" +
                      row["CompanyName", DataRowVersion.Original] + ", " +
                      (row.IsNull("ContactName") ? "NULL" : row["ContactName", DataRowVersion.Original].ToString());
            }
            else
            {
                msg = "Ошибка: Удалённая строка не содержит оригинальных данных.";
            }
        }
        else if (row.RowState == DataRowState.Modified)
        {
            msg = "Original data:\n" +
                  row["CompanyName", DataRowVersion.Original] + ", " +
                  (row.IsNull("ContactName") ? "NULL" : row["ContactName", DataRowVersion.Original].ToString()) + "\n";

            msg += "Current data:\n" +
                   row["CompanyName", DataRowVersion.Current] + ", " +
                   (row.IsNull("ContactName") ? "NULL" : row["ContactName", DataRowVersion.Current].ToString());
        }

        MessageBox.Show(msg, "RowState: " + row.RowState);
    }
}








// Data Modification Commands


// Using the InsertCommand property
NpgsqlCommand cmInsert = new NpgsqlCommand(
    "INSERT INTO Customers VALUES (@ID, @Name)",
    cnNorthwind);
cmInsert.Parameters.Add(new SqlParameter("@ID",
    SqlDbType.NChar, 5, ParameterDirection.Input, false, 
    0, 0, "CustomerID", DataRowVersion.Current, null));
cmInsert.Parameters.Add(new SqlParameter("@Name",
    SqlDbType.NVarChar, 40, ParameterDirection.Input, false,
    0, 0, "CompanyName", DataRowVersion.Current, null));
daCustomers.InsertCommand = cmInsert;

// Using the UpdateCommand property
NpgsqlCommand cmUpdate = new NpgsqlCommand(
    "UPDATE Customers SET CustomerID = @ID, " +
    "CompanyName = @Name WHERE (CustomerID = @OrigID)",
    cnNorthwind);
cmUpdate.Parameters.Add(new SqlParameter("@ID",
    SqlDbType.NChar, 5, ParameterDirection.Input, false,
    0, 0, "CustomerID", DataRowVersion.Current, null));
cmUpdate.Parameters.Add(new SqlParameter("@Name",
    SqlDbType.NVarChar, 40, ParameterDirection.Input, false,
    0, 0, "CompanyName", DataRowVersion.Current, null));
cmUpdate.Parameters.Add(new SqlParameter("@OrigID",
    SqlDbType.NChar, 5, ParameterDirection.Input, false,
    0, 0, "CustomerID", DataRowVersion.Original, null));
daCustomers.UpdateCommand = cmUpdate;

// Using the DeleteCommand
NpgsqlCommand cmDelete = new NpgsqlCommand(
    "DELETE FROM Customers WHERE (CustomerID = @ID)",
    cnNorthwind);
cmDelete.Parameters.Add(new SqlParameter("@ID",
    SqlDbType.NChar, 5, ParameterDirection.Input, false, 
    0, 0, "CustomerID", DataRowVersion.Original, null));
daCustomers.DeleteCommand = cmDelete;


// программно устанавливает команды InsertCommand, UpdateCommand, DeleteCommand для NpgsqlDataAdapter

// InsertCommand
NpgsqlCommand cmInsert = new NpgsqlCommand(
    "INSERT INTO \"Customers\" (\"CustomerID\", \"CompanyName\") VALUES (@ID, @Name)", cnNorthwind);
cmInsert.Parameters.Add(new NpgsqlParameter("@ID",
    NpgsqlTypes.NpgsqlDbType.Char) { SourceColumn = "CustomerID" });
cmInsert.Parameters.Add(new NpgsqlParameter("@Name",
    NpgsqlTypes.NpgsqlDbType.Varchar) { SourceColumn = "CompanyName" });
daCustomers.InsertCommand = cmInsert;

// UpdateCommand
NpgsqlCommand cmUpdate = new NpgsqlCommand(
    "UPDATE \"Customers\" SET \"CompanyName\" = @Name WHERE \"CustomerID\" = @OrigID", cnNorthwind);
cmUpdate.Parameters.Add(new NpgsqlParameter("@Name",
    NpgsqlTypes.NpgsqlDbType.Varchar) { SourceColumn = "CompanyName" });
cmUpdate.Parameters.Add(new NpgsqlParameter("@OrigID",
    NpgsqlTypes.NpgsqlDbType.Char) { SourceColumn = "CustomerID", SourceVersion = DataRowVersion.Original });
daCustomers.UpdateCommand = cmUpdate;

// DeleteCommand
NpgsqlCommand cmDelete = new NpgsqlCommand(
    "DELETE FROM \"Customers\" WHERE \"CustomerID\" = @ID", cnNorthwind);
cmDelete.Parameters.Add(new NpgsqlParameter("@ID",
    NpgsqlTypes.NpgsqlDbType.Char) { SourceColumn = "CustomerID", SourceVersion = DataRowVersion.Original });
daCustomers.DeleteCommand = cmDelete;









// Updating a Data Source


// Fill the Customers and Orders tables initially
daCustomers.Fill(dsCustomerOrders.Customers);
daOrders.Fill(dsCustomerOrders.Orders);
dataGrid1.DataSource = dsCustomerOrders.Customers.DefaultView;

// Update the data source with any changes
DataTable deletedOrders = 
  dsCustomerOrders.Orders.GetChanges(DataRowState.Deleted);
daOrders.Update(deletedOrders);

DataTable deletedCustomers = 
  dsCustomerOrders.Customers.GetChanges(DataRowState.Deleted);
daCustomers.Update(deletedCustomers);


// обновляет источник данных DataSet после внесения изменений DataTable

// проверка NullReferenceException

using System;
using System.Data;
using Npgsql;

private void UpdateDatabase()
{
    try
    {
        // Заполняем DataSet данными
        daCustomers.Fill(dsCustomerOrders.Customers);
        daOrders.Fill(dsCustomerOrders.Orders);
        dataGrid1.DataSource = dsCustomerOrders.Customers.DefaultView;

        // Удаление данных 1
        DataTable deletedOrders = dsCustomerOrders.Orders.GetChanges(DataRowState.Deleted);
        if (deletedOrders != null)
        {
            daOrders.Update(deletedOrders);
            deletedOrders.AcceptChanges();
        }

        // Удаление данных 2
        DataTable deletedCustomers = dsCustomerOrders.Customers.GetChanges(DataRowState.Deleted);
        if (deletedCustomers != null)
        {
            daCustomers.Update(deletedCustomers);
            deletedCustomers.AcceptChanges();
        }

        // Обновляем изменённые и новые записи
        daOrders.Update(dsCustomerOrders.Orders);
        daCustomers.Update(dsCustomerOrders.Customers);

        Console.WriteLine("Обновление базы данных завершено.");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Ошибка при обновлении данных: " + ex.Message);
    }
}








// Merging DataSets


// Code, at the client

// Get changes made by the user to the dsCustomers DataSet
DataSet dsChanges = dsCustomers.GetChanges();
			
// Send changes to a Web Service, get latest data back again
MyWebService service = new MyWebService();
DataSet dsLatest = service.MyUpdateMethod(dsChanges);
			
// Merge latest data back into the dsCustomers DataSet
dsCustomers.Merge(dsLatest);

// Mark all rows as "unchanged" in the dsCustomers DataSet
dsCustomers.AcceptChanges();


// изменения, сделанные на клиенте, отправляются на сервер/веб-сервис, и объединение DataSet

private void SyncWithServer()
{
    try
    {
        // Получаем изменения
        DataSet dsChanges = dsCustomers.GetChanges();

        if (dsChanges == null || dsChanges.Tables.Count == 0)
        {
            Console.WriteLine("Нет изменений для отправки.");
            return;
        }

        // Отправляем изменения в веб-сервис
        MyWebService service = new MyWebService();
        DataSet dsLatest = service.MyUpdateMethod(dsChanges);

        if (dsLatest != null)
        {
            // Объединяем обновлённые данные с клиентским DataSet
            dsCustomers.Merge(dsLatest);
        }

        // Фиксируем изменения (если не планируем повторное обновление)
        dsCustomers.AcceptChanges();

        Console.WriteLine("Синхронизация завершена.");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Ошибка при синхронизации: " + ex.Message);
    }
}








// How the Data Adapter Configuration Wizard Supports Optimistic Concurrency


this.cmUpdate.CommandText = 
   "UPDATE Customers " +
   "SET CustomerID=@CustomerID, CompanyName=@CompanyName " +
   "   WHERE (CustomerID  = @Original_CustomerID) " +
   "   AND   (CompanyName = @Original_CompanyName); " +
   "SELECT CustomerID, CompanyName FROM Customers " +
   "   WHERE (CustomerID = @Select_CustomerID)";

this.cmUpdate.Parameters.Add(new SqlParameter(
  "@CustomerID",
   SqlDbType.NChar, 5, ParameterDirection.Input, false, 
   0, 0, "CustomerID", DataRowVersion.Current, null));

this.cmUpdate.Parameters.Add(new SqlParameter(
  "@CompanyName", 
   SqlDbType.NVarChar, 40, ParameterDirection.Input, false, 
   0, 0, "CompanyName", DataRowVersion.Current, null));

this.cmUpdate.Parameters.Add(new SqlParameter(
  "@Original_CustomerID",
   SqlDbType.NChar, 5, ParameterDirection.Input, false,
   0, 0 , "CustomerID", DataRowVersion.Original, null));

this.cmUpdate.Parameters.Add(new SqlParameter(
  "@Original_CompanyName", 
   SqlDbType.NVarChar, 40, ParameterDirection.Input, false, 
   0, 0, "CompanyName", DataRowVersion.Original, null));

this.cmUpdate.Parameters.Add(new SqlParameter(
  "@Select_CustomerID", 
   SqlDbType.NChar, 5, ParameterDirection.Input, false, 
   0, 0, "CustomerID", DataRowVersion.Current, null));


// @CustomerID Primary Key обычно не меняют, используя оптимистичную конкуренцию

// SourceVersion = DataRowVersion.Original для проверки изменений

private void ConfigureUpdateCommand(NpgsqlCommand cmUpdate)
{
    cmUpdate.CommandText = @"
        UPDATE ""Customers"" 
        SET ""CompanyName"" = @CompanyName
        WHERE (""CustomerID"" = @Original_CustomerID) 
          AND (""CompanyName"" = @Original_CompanyName);
        SELECT ""CustomerID"", ""CompanyName"" FROM ""Customers"" 
        WHERE (""CustomerID"" = @Select_CustomerID);";

    cmUpdate.Parameters.Add(new NpgsqlParameter(
        "@CustomerID", NpgsqlTypes.NpgsqlDbType.Char, 5) { SourceColumn = "CustomerID" });

    cmUpdate.Parameters.Add(new NpgsqlParameter(
        "@CompanyName", NpgsqlTypes.NpgsqlDbType.Varchar, 40) { SourceColumn = "CompanyName" });

    cmUpdate.Parameters.Add(new NpgsqlParameter(
        "@Original_CustomerID", NpgsqlTypes.NpgsqlDbType.Char, 5) { 
        SourceColumn = "CustomerID", SourceVersion = DataRowVersion.Original });

    cmUpdate.Parameters.Add(new NpgsqlParameter(
        "@Original_CompanyName", NpgsqlTypes.NpgsqlDbType.Varchar, 40) { 
        SourceColumn = "CompanyName", SourceVersion = DataRowVersion.Original });

    cmUpdate.Parameters.Add(new NpgsqlParameter(
        "@Select_CustomerID", NpgsqlTypes.NpgsqlDbType.Char, 5) { 
        SourceColumn = "CustomerID", SourceVersion = DataRowVersion.Current });
}













// Resolving Conflicts in a Disconnected Environment


try 
{
  daCustomers.Update(dsCustomers);
}
catch(System.Exception ex) 
{
  if(dsCustomers.HasErrors)
  {
    foreach(DataTable table in dsCustomers.Tables)
    {
      if(table.HasErrors)
      {
        foreach(DataRow row in table.Rows)
        {
          if(row.HasErrors)
          {
            MessageBox.Show("Row: " + row["CustomerID"],
                             row.RowError);

            foreach(DataColumn col in row.GetColumnsInError())
            {
              MessageBox.Show(col.ColumnName, 
                              "Error in this column");
            }
            row.ClearErrors();
            row.RejectChanges();
          }
        }
      }
    }
  }
} 

// разрешение конфликтов в отключённом режиме при обновлении DataSet

// повтроная попытка Update() из Enterprise-практики, если возможны конфликты данных

private void UpdateDatabase()
{
    int maxRetries = 3; // Максимальное число попыток
    int retryDelay = 1000; // Пауза перед повторной попыткой (мс)

    for (int attempt = 1; attempt <= maxRetries; attempt++)
    {
        try
        {
            daCustomers.Update(dsCustomers);
            Console.WriteLine("Обновление выполнено успешно.");
            break; // Выход из цикла при успешном обновлении
        }
        catch (DBConcurrencyException ex) // Конфликт при обновлении (Optimistic Concurrency)
        {
            Console.WriteLine($"Попытка {attempt}: Конфликт при обновлении - {ex.Message}");

            // Отображаем конфликтующие строки
            foreach (DataTable table in dsCustomers.Tables)
            {
                if (table.HasErrors)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        if (row.HasErrors)
                        {
                            Console.WriteLine($"Ошибка в строке {row["CustomerID"]}: {row.RowError}");

                            foreach (DataColumn col in row.GetColumnsInError())
                            {
                                Console.WriteLine($"Ошибка в колонке: {col.ColumnName}");
                            }

                            // Очищаем ошибку, загружаем актуальные данные
                            row.ClearErrors();
                            row.RejectChanges(); // Откат изменений в этой строке
                        }
                    }
                }
            }

            // Перезагружаем актуальные данные перед повторной попыткой
            Console.WriteLine("Обновляем DataSet новыми данными...");
            daCustomers.Fill(dsCustomers, "Customers");

            // Пауза перед повторной попыткой
            Thread.Sleep(retryDelay);
        }
        catch (NpgsqlException ex) // Ошибки PostgreSQL
        {
            Console.WriteLine($"Ошибка базы данных: {ex.Message}");

            // Обрабатываем типичные ошибки
            if (ex.SqlState == "40001") // Deadlock
            {
                Console.WriteLine("Обнаружен Deadlock. Повторная попытка...");
                Thread.Sleep(retryDelay);
                continue; // Повторяем попытку
            }
            else if (ex.SqlState == "23505") // Нарушение уникальности (Duplicate Key)
            {
                Console.WriteLine("Ошибка: Дублирующийся ключ. Отмена обновления.");
                break; // Прерываем обновление
            }
            else
            {
                Console.WriteLine("Критическая ошибка. Прерываем обновление.");
                break;
            }
        }
        catch (Exception ex) // Все остальные ошибки
        {
            Console.WriteLine($"Неизвестная ошибка: {ex.Message}");
            break; // Не пытаемся повторять
        }
    }
}











