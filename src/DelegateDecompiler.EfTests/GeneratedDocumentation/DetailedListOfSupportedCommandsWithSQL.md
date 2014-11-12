Detail With Sql of supported commands
============
## Documentation produced for DelegateDecompiler, version 0.11.1.0 on 12 November 2014 08:55

This file documents what linq commands **DelegateDecompiler** supports when
working with [Entity Framework v6.1](http://msdn.microsoft.com/en-us/data/aa937723) (EF).
EF has one of the best implementations for converting Linq `IQueryable<>` commands into database
access commands, in EF's case T-SQL. Therefore it is a good candidate for using in our tests.

This documentation was produced by compaired direct EF Linq queries against the same query implemented
as a DelegateDecompiler's `Computed` properties. This produces a Supported/Not Supported flag
on each command type tested. Tests are groups and ordered to try and make finding things
easier.

So, if you want to use DelegateDecompiler and are not sure whether the linq command
you want to use will work then clone this project and write your own tests.
(See [How to add a test](HowToAddMoreTests.md) documentation on how to do this). 
If there is a problem then please fork the repository and add your own tests. 
That will make it much easier to diagnose your issue.

*Note: The test suite has only recently been set up and has only a handful of tests at the moment.
More will appear as we move forward.*


### Group: Logical Operators
#### [Boolean](../TestGroup05LogicalOperators/Test01Boolean.cs):
- Supported
  * Bool Equals Constant (line 32)
     * T-Sql executed is

```SQL
SELECT 
    [Extent1].[ParentBool] AS [ParentBool]
    FROM [dbo].[EfParents] AS [Extent1]
```

  * Bool Equals Static Variable (line 51)
     * T-Sql executed is

```SQL
SELECT 
    CASE WHEN ([Extent1].[ParentBool] = @p__linq__0) THEN cast(1 as bit) WHEN ([Extent1].[ParentBool] <> @p__linq__0) THEN cast(0 as bit) END AS [C1]
    FROM [dbo].[EfParents] AS [Extent1]
```

  * Int Equals Constant (line 68)
     * T-Sql executed is

```SQL
SELECT 
    CASE WHEN (123 = [Extent1].[ParentInt]) THEN cast(1 as bit) WHEN (123 <> [Extent1].[ParentInt]) THEN cast(0 as bit) END AS [C1]
    FROM [dbo].[EfParents] AS [Extent1]
```



### Group: Equality Operators
#### [Equals And Not Equals](../TestGroup06EqualityOperators/Test01EqualsAndNotEquals.cs):
- Supported
  * Int Equals Constant (line 32)
     * T-Sql executed is

```SQL
SELECT 
    CASE WHEN (123 = [Extent1].[ParentInt]) THEN cast(1 as bit) WHEN (123 <> [Extent1].[ParentInt]) THEN cast(0 as bit) END AS [C1]
    FROM [dbo].[EfParents] AS [Extent1]
```

  * Int Equals Static Variable (line 50)
     * T-Sql executed is

```SQL
SELECT 
    CASE WHEN ([Extent1].[ParentInt] = @p__linq__0) THEN cast(1 as bit) WHEN ([Extent1].[ParentInt] <> @p__linq__0) THEN cast(0 as bit) END AS [C1]
    FROM [dbo].[EfParents] AS [Extent1]
```

  * Int Equals String Length (line 67)
     * T-Sql executed is

```SQL
SELECT 
    CASE WHEN ([Extent1].[ParentInt] = ( CAST(LEN([Extent1].[ParentString]) AS int))) THEN cast(1 as bit) WHEN ( NOT (([Extent1].[ParentInt] = ( CAST(LEN([Extent1].[ParentString]) AS int))) AND (0 = (CASE WHEN ([Extent1].[ParentString] IS NULL) THEN cast(1 as bit) ELSE cast(0 as bit) END)))) THEN cast(0 as bit) END AS [C1]
    FROM [dbo].[EfParents] AS [Extent1]
```

  * Int Not Equals String Length (line 84)
     * T-Sql executed is

```SQL
SELECT 
    CASE WHEN (0 = (CASE WHEN ([Extent1].[ParentInt] = ( CAST(LEN([Extent1].[ParentString]) AS int))) THEN cast(1 as bit) WHEN ( NOT (([Extent1].[ParentInt] = ( CAST(LEN([Extent1].[ParentString]) AS int))) AND (0 = (CASE WHEN ([Extent1].[ParentString] IS NULL) THEN cast(1 as bit) ELSE cast(0 as bit) END)))) THEN cast(0 as bit) END)) THEN cast(1 as bit) WHEN ( NOT ((0 = (CASE WHEN ([Extent1].[ParentInt] = ( CAST(LEN([Extent1].[ParentString]) AS int))) THEN cast(1 as bit) WHEN ( NOT (([Extent1].[ParentInt] = ( CAST(LEN([Extent1].[ParentString]) AS int))) AND (0 = (CASE WHEN ([Extent1].[ParentString] IS NULL) THEN cast(1 as bit) ELSE cast(0 as bit) END)))) THEN cast(0 as bit) END)) AND (CASE WHEN ([Extent1].[ParentInt] = ( CAST(LEN([Extent1].[ParentString]) AS int))) THEN cast(1 as bit) WHEN ( NOT (([Extent1].[ParentInt] = ( CAST(LEN([Extent1].[ParentString]) AS int))) AND (0 = (CASE WHEN ([Extent1].[ParentString] IS NULL) THEN cast(1 as bit) ELSE cast(0 as bit) END)))) THEN cast(0 as bit) END IS NOT NULL))) THEN cast(0 as bit) END AS [C1]
    FROM [dbo].[EfParents] AS [Extent1]
```



### Group: Quantifier Operators
#### [Any](../TestGroup12QuantifierOperators/Test01Any.cs):
- Supported
  * Any Children (line 32)
     * T-Sql executed is

```SQL
SELECT 
    CASE WHEN ( EXISTS (SELECT 
        1 AS [C1]
        FROM [dbo].[EfChilds] AS [Extent2]
        WHERE [Extent1].[EfParentId] = [Extent2].[EfParentId]
    )) THEN cast(1 as bit) ELSE cast(0 as bit) END AS [C1]
    FROM [dbo].[EfParents] AS [Extent1]
```

  * Any Children With Filter (line 49)
     * T-Sql executed is

```SQL
SELECT 
    CASE WHEN ( EXISTS (SELECT 
        1 AS [C1]
        FROM [dbo].[EfChilds] AS [Extent2]
        WHERE ([Extent1].[EfParentId] = [Extent2].[EfParentId]) AND (123 = [Extent2].[ChildInt])
    )) THEN cast(1 as bit) ELSE cast(0 as bit) END AS [C1]
    FROM [dbo].[EfParents] AS [Extent1]
```


#### [All](../TestGroup12QuantifierOperators/Test02All.cs):
- Supported
  * Singleton All Filter (line 32)
     * T-Sql executed is

```SQL
SELECT 
    CASE WHEN ( NOT EXISTS (SELECT 
        1 AS [C1]
        FROM [dbo].[EfParents] AS [Extent1]
        WHERE (123 <> [Extent1].[ParentInt]) OR (CASE WHEN (123 = [Extent1].[ParentInt]) THEN cast(1 as bit) WHEN (123 <> [Extent1].[ParentInt]) THEN cast(0 as bit) END IS NULL)
    )) THEN cast(1 as bit) ELSE cast(0 as bit) END AS [C1]
    FROM  ( SELECT 1 AS X ) AS [SingleRowTable1]
```

  * All Filter On Children Int (line 49)
     * T-Sql executed is

```SQL
SELECT 
    CASE WHEN ( NOT EXISTS (SELECT 
        1 AS [C1]
        FROM [dbo].[EfChilds] AS [Extent2]
        WHERE ([Extent1].[EfParentId] = [Extent2].[EfParentId]) AND ((123 <> [Extent2].[ChildInt]) OR (CASE WHEN (123 = [Extent2].[ChildInt]) THEN cast(1 as bit) WHEN (123 <> [Extent2].[ChildInt]) THEN cast(0 as bit) END IS NULL))
    )) THEN cast(1 as bit) ELSE cast(0 as bit) END AS [C1]
    FROM [dbo].[EfParents] AS [Extent1]
```


#### [Contains](../TestGroup12QuantifierOperators/Test03Contains.cs):
- Supported
  * String Contains Constant String With Filter (line 33)
     * T-Sql executed is

```SQL
SELECT 
    [Extent1].[ParentString] AS [ParentString]
    FROM [dbo].[EfParents] AS [Extent1]
    WHERE [Extent1].[ParentString] LIKE N'%2%'
```



### Group: Aggregation
#### [Count](../TestGroup15Aggregation/Test01Count.cs):
- Supported
  * Count Children (line 33)
     * T-Sql executed is

```SQL
SELECT 
    (SELECT 
        COUNT(1) AS [A1]
        FROM [dbo].[EfChilds] AS [Extent2]
        WHERE [Extent1].[EfParentId] = [Extent2].[EfParentId]) AS [C1]
    FROM [dbo].[EfParents] AS [Extent1]
```

  * Count Children With Filter (line 51)
     * T-Sql executed is

```SQL
SELECT 
    (SELECT 
        COUNT(1) AS [A1]
        FROM [dbo].[EfChilds] AS [Extent2]
        WHERE ([Extent1].[EfParentId] = [Extent2].[EfParentId]) AND (123 = [Extent2].[ChildInt])) AS [C1]
    FROM [dbo].[EfParents] AS [Extent1]
```

  * Singleton Count Children With Filter (line 69)
     * T-Sql executed is

```SQL
SELECT 
    [GroupBy2].[A1] AS [C1]
    FROM ( SELECT 
        COUNT(1) AS [A1]
        FROM ( SELECT 
            (SELECT 
                COUNT(1) AS [A1]
                FROM [dbo].[EfChilds] AS [Extent2]
                WHERE [Extent1].[EfParentId] = [Extent2].[EfParentId]) AS [C1]
            FROM [dbo].[EfParents] AS [Extent1]
        )  AS [Project1]
        WHERE 2 = [Project1].[C1]
    )  AS [GroupBy2]
```


#### [Sum](../TestGroup15Aggregation/Test02Sum.cs):
- Supported
  * Singleton Sum Children (line 33)
     * T-Sql executed is

```SQL
SELECT 
    [GroupBy2].[A1] AS [C1]
    FROM ( SELECT 
        SUM([Extent1].[A1]) AS [A1]
        FROM ( SELECT 
            (SELECT 
                COUNT(1) AS [A1]
                FROM [dbo].[EfChilds] AS [Extent2]
                WHERE [Extent1].[EfParentId] = [Extent2].[EfParentId]) AS [A1]
            FROM [dbo].[EfParents] AS [Extent1]
        )  AS [Extent1]
    )  AS [GroupBy2]
```

- **Not Supported**
  * Sum Count In Children Where Children Can Be None (line 47)


### Group: Order Take
#### [Order By](../TestGroup20OrderTake/Test01OrderBy.cs):
- Supported
  * Order By Children Count (line 33)
     * T-Sql executed is

```SQL
SELECT 
    [Project1].[EfParentId] AS [EfParentId]
    FROM ( SELECT 
        [Extent1].[EfParentId] AS [EfParentId], 
        (SELECT 
            COUNT(1) AS [A1]
            FROM [dbo].[EfChilds] AS [Extent2]
            WHERE [Extent1].[EfParentId] = [Extent2].[EfParentId]) AS [C1]
        FROM [dbo].[EfParents] AS [Extent1]
    )  AS [Project1]
    ORDER BY [Project1].[C1] ASC
```

  * Order By Children Count Then By String Length (line 51)
     * T-Sql executed is

```SQL
SELECT 
    [Project2].[EfParentId] AS [EfParentId]
    FROM ( SELECT 
         CAST(LEN([Project1].[ParentString]) AS int) AS [C1], 
        [Project1].[EfParentId] AS [EfParentId], 
        [Project1].[C1] AS [C2]
        FROM ( SELECT 
            [Extent1].[EfParentId] AS [EfParentId], 
            [Extent1].[ParentString] AS [ParentString], 
            (SELECT 
                COUNT(1) AS [A1]
                FROM [dbo].[EfChilds] AS [Extent2]
                WHERE [Extent1].[EfParentId] = [Extent2].[EfParentId]) AS [C1]
            FROM [dbo].[EfParents] AS [Extent1]
        )  AS [Project1]
    )  AS [Project2]
    ORDER BY [Project2].[C2] ASC, [Project2].[C1] ASC
```

  * Where Any Children Then Order By Children Count (line 69)
     * T-Sql executed is

```SQL
SELECT 
    [Project2].[EfParentId] AS [EfParentId]
    FROM ( SELECT 
        [Extent1].[EfParentId] AS [EfParentId], 
        (SELECT 
            COUNT(1) AS [A1]
            FROM [dbo].[EfChilds] AS [Extent3]
            WHERE [Extent1].[EfParentId] = [Extent3].[EfParentId]) AS [C1]
        FROM [dbo].[EfParents] AS [Extent1]
        WHERE  EXISTS (SELECT 
            1 AS [C1]
            FROM [dbo].[EfChilds] AS [Extent2]
            WHERE [Extent1].[EfParentId] = [Extent2].[EfParentId]
        )
    )  AS [Project2]
    ORDER BY [Project2].[C1] ASC
```


#### [Skip Take](../TestGroup20OrderTake/Test02SkipTake.cs):
- Supported
  * Order By Children Count Then Take (line 33)
     * T-Sql executed is

```SQL
SELECT TOP (2) 
    [Project1].[EfParentId] AS [EfParentId]
    FROM ( SELECT 
        [Extent1].[EfParentId] AS [EfParentId], 
        (SELECT 
            COUNT(1) AS [A1]
            FROM [dbo].[EfChilds] AS [Extent2]
            WHERE [Extent1].[EfParentId] = [Extent2].[EfParentId]) AS [C1]
        FROM [dbo].[EfParents] AS [Extent1]
    )  AS [Project1]
    ORDER BY [Project1].[C1] ASC
```

  * Order By Children Count Then Skip And Take (line 51)
     * T-Sql executed is

```SQL
SELECT TOP (2) 
    [Project1].[EfParentId] AS [EfParentId]
    FROM ( SELECT [Project1].[EfParentId] AS [EfParentId], [Project1].[C1] AS [C1], row_number() OVER (ORDER BY [Project1].[C1] ASC) AS [row_number]
        FROM ( SELECT 
            [Extent1].[EfParentId] AS [EfParentId], 
            (SELECT 
                COUNT(1) AS [A1]
                FROM [dbo].[EfChilds] AS [Extent2]
                WHERE [Extent1].[EfParentId] = [Extent2].[EfParentId]) AS [C1]
            FROM [dbo].[EfParents] AS [Extent1]
        )  AS [Project1]
    )  AS [Project1]
    WHERE [Project1].[row_number] > 1
    ORDER BY [Project1].[C1] ASC
```

  * Where Any Children Then Order By Then Skip Take (line 69)
     * T-Sql executed is

```SQL
SELECT TOP (1) 
    [Project2].[EfParentId] AS [EfParentId]
    FROM ( SELECT [Project2].[EfParentId] AS [EfParentId], [Project2].[C1] AS [C1], row_number() OVER (ORDER BY [Project2].[C1] ASC) AS [row_number]
        FROM ( SELECT 
            [Extent1].[EfParentId] AS [EfParentId], 
            (SELECT 
                COUNT(1) AS [A1]
                FROM [dbo].[EfChilds] AS [Extent3]
                WHERE [Extent1].[EfParentId] = [Extent3].[EfParentId]) AS [C1]
            FROM [dbo].[EfParents] AS [Extent1]
            WHERE  EXISTS (SELECT 
                1 AS [C1]
                FROM [dbo].[EfChilds] AS [Extent2]
                WHERE [Extent1].[EfParentId] = [Extent2].[EfParentId]
            )
        )  AS [Project2]
    )  AS [Project2]
    WHERE [Project2].[row_number] > 1
    ORDER BY [Project2].[C1] ASC
```



### Group: Types
#### [Strings](../TestGroup50Types/Test01Strings.cs):
- Supported
  * Concatenate Person Not Handle Null (line 32)
     * T-Sql executed is

```SQL
SELECT 
    [Extent1].[FirstName] + N' ' + CASE WHEN ([Extent1].[MiddleName] IS NULL) THEN N'' ELSE [Extent1].[MiddleName] END + N' ' + [Extent1].[LastName] AS [C1]
    FROM [dbo].[EfPersons] AS [Extent1]
```

  * Concatenate Person Handle Null (line 49)
     * T-Sql executed is

```SQL
SELECT 
    [Extent1].[FirstName] + CASE WHEN (CASE WHEN ([Extent1].[MiddleName] IS NULL) THEN N'' ELSE N' ' END IS NULL) THEN N'' WHEN ([Extent1].[MiddleName] IS NULL) THEN N'' ELSE N' ' END + CASE WHEN ([Extent1].[MiddleName] IS NULL) THEN N'' ELSE [Extent1].[MiddleName] END + N' ' + [Extent1].[LastName] AS [C1]
    FROM [dbo].[EfPersons] AS [Extent1]
```

  * Concatenate Person Handle Name Order (line 68)
     * T-Sql executed is

```SQL
SELECT 
    CASE WHEN (cast(0 as bit) <> [Extent1].[NameOrder]) THEN [Extent1].[LastName] + N', ' + [Extent1].[FirstName] + CASE WHEN (CASE WHEN ([Extent1].[MiddleName] IS NULL) THEN N'' ELSE N' ' END IS NULL) THEN N'' WHEN ([Extent1].[MiddleName] IS NULL) THEN N'' ELSE N' ' END ELSE [Extent1].[FirstName] + CASE WHEN (CASE WHEN ([Extent1].[MiddleName] IS NULL) THEN N'' ELSE N' ' END IS NULL) THEN N'' WHEN ([Extent1].[MiddleName] IS NULL) THEN N'' ELSE N' ' END + CASE WHEN ([Extent1].[MiddleName] IS NULL) THEN N'' ELSE [Extent1].[MiddleName] END + N' ' + [Extent1].[LastName] END AS [C1]
    FROM [dbo].[EfPersons] AS [Extent1]
```


#### [DateTime](../TestGroup50Types/Test05DateTime.cs):
- Supported
  * DateTime Where Compare With Static Variable (line 35)
     * T-Sql executed is

```SQL
SELECT 
    [Extent1].[StartDate] AS [StartDate]
    FROM [dbo].[EfParents] AS [Extent1]
    WHERE [Extent1].[StartDate] > @p__linq__0
```




The End
