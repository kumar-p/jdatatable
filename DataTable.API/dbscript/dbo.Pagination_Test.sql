CREATE PROCEDURE dbo.Pagination_Test -- ORDER BY CustomerID
  @PageNumber INT = 1,
  @PageSize   INT = 100
AS
BEGIN
  SET NOCOUNT ON;
 
  SELECT CustomerID, CompanyName, ContactName, City, Country, Phone	
    FROM dbo.Customers
    ORDER BY CustomerID
    OFFSET @PageSize * (@PageNumber - 1) ROWS
    FETCH NEXT @PageSize ROWS ONLY OPTION (RECOMPILE);
END