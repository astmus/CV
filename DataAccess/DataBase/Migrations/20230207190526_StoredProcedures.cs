using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class StoredProcedures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        
		    var createFindProcSql = @"CREATE OR ALTER PROCEDURE [dbo].FindCustomers
                    @SearchName NVARCHAR(50) = null, @SearchCompany NVARCHAR(50) = null, @SearchEmail NVARCHAR(50) = null, @SearchPhone NVARCHAR(50) = null, @Page INT = 0,@PageCount INT = 10,@SortBy NVARCHAR(50) = null, @SortDesc INT = 0
                        AS
                        BEGIN
                            SELECT * FROM Customers
                            WHERE
                                (@SearchName = '' OR [Name] LIKE '%'+@SearchName+'%') AND
                                (@SearchCompany = '' OR [CompanyName] LIKE '%'+@SearchCompany+'%') AND
                                (@SearchEmail = '' OR [Email] LIKE '%'+@SearchEmail+'%') AND
                                (@SearchPhone = '' OR [Phone] LIKE '%'+@SearchPhone+'%')
                            ORDER BY CASE @SortDesc
                                WHEN 1 THEN
                                    CASE @SortBy
                                        WHEN 'Name' THEN [Name]
                                        WHEN 'CompanyName' THEN [CompanyName]
                                        WHEN 'Email' THEN [Email]
                                        WHEN 'Phone' THEN [Phone]
                                    END
                                    END DESC,
                                    CASE WHEN @SortDesc = 0 THEN
                                        CASE @SortBy
                                            WHEN 'Name' THEN [Name]
                                            WHEN 'CompanyName' THEN [CompanyName]
                                            WHEN 'Email' THEN [Email]
                                            WHEN 'Phone' THEN [Phone]
                                            END
                                    END
                            OFFSET @Page * @PageCount ROWS FETCH NEXT @PageCount ROWS ONLY;
                        END";
			migrationBuilder.Sql(createFindProcSql);
			var createCountProcSql = @"CREATE OR ALTER PROCEDURE [dbo].CountCustomers
                    @SearchName NVARCHAR(50) = '', @SearchCompany NVARCHAR(50) = '', @SearchEmail NVARCHAR(50) = '', @SearchPhone NVARCHAR(50) = ''
                        AS
                        BEGIN
                            SELECT count(*) FROM Customers
                            WHERE
                                (@SearchName = '' OR [Name] LIKE '%'+@SearchName+'%') AND
                                (@SearchCompany = '' OR [CompanyName] LIKE '%'+@SearchCompany+'%') AND
                                (@SearchEmail = '' OR [Email] LIKE '%'+@SearchEmail+'%') AND
                                (@SearchPhone = '' OR [Phone] LIKE '%'+@SearchPhone+'%')                            
                        END";
			migrationBuilder.Sql(createCountProcSql);
		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
