// NpgsqlConnection Example

Npgsql.NpgsqlConnection cnNorthwind =
  new Npgsql.NpgsqlConnection();

cnNorthwind.ConnectionString = 
  "Host=localhost;" +
  "Port=5432;" +
  "Username=sa;" +
  "Password=2389;" +
  "Database=Northwind;" +
  "Timeout=60;";







// OleDbConnection Example


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


// Пример использование 

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




















































