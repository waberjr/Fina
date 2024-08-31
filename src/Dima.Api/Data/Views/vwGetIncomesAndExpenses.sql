CREATE OR ALTER VIEW [vwGetIncomesAndExpenses] AS
SELECT [Transactions].[UserEmail],
    MONTH([Transactions].[PaidOrReceivedAt])                                         AS [Month],
    YEAR([Transactions].[PaidOrReceivedAt])                                          AS [Year],
    SUM(CASE WHEN [Transactions].[Type] = 1 THEN [Transactions].[Amount] ELSE 0 END) AS [Incomes],
    SUM(CASE WHEN [Transactions].[Type] = 2 THEN [Transactions].[Amount] ELSE 0 END) AS [Expenses]
FROM [Transactions]
WHERE [Transactions].[PaidOrReceivedAt]
    >= DATEADD(MONTH, -11, CAST(GETDATE() AS DATE))
  AND [Transactions].[PaidOrReceivedAt]
    < DATEADD(MONTH, 1, CAST(GETDATE() AS DATE))
GROUP BY [Transactions].[UserEmail],
    MONTH([Transactions].[PaidOrReceivedAt]),
    YEAR([Transactions].[PaidOrReceivedAt])