SELECT
	'0-004-4060-000' AS [Epson COGS],
	header.CUSTNMBR AS [Customer No],
	isnull(enVisionNumber.ResellerNo, '') AS [Reseller No],
	header.CUSTNAME AS [Reseller Name],
	header.CNTCPRSN AS [End User Name],
	header.ACTLSHIP AS [Invoice Date],
	header.SOPNUMBE AS [Invoice Number],
	isnull(gpItems.ITMSHNAM, lineItems.ITEMNMBR) AS [Part],
	lineItems.ITEMNMBR AS [Item Number],
	serialTable.CMMTTEXT AS [Serial No.],
	lineItems.QTYFULFI AS [Units],
	header.SLPRSNID AS [SALESREPID],
	custMaster.ADDRESS1 AS [Cust Address],
	custMaster.CITY AS [Cust City],
	custMaster.STATE AS [Cust State],
	custMaster.ZIP AS [Cust Zip],
/*	lineItems.CNTCPRSN AS [ShipTo Cust],*/
	header.ADDRESS2 AS [ShipTo Address],
	header.CITY AS [ShipTo City],
	header.STATE AS [ShipTo State],
	header.ZIPCODE AS [ShipTo Zip]

	FROM
		/*
		  	This SELECT statement creates the header
		  	table, which gets all invoices created in
		  	a specific month & year, and is our primary
		  	left-most table
		 */
		(SELECT
			tHeader.SOPNUMBE,
			tHeader.ACTLSHIP,
			tHeader.CUSTNMBR,
			tHeader.CUSTNAME,
			tHeader.CNTCPRSN,
			tHeader.SLPRSNID,
			tHeader.ADDRESS2,
			tHeader.CITY,
			tHeader.STATE,
			tHeader.ZIPCODE
			
			FROM METRO.dbo.SOP30200 tHeader
			WHERE
				MONTH(ACTLSHIP) = {0}				/*CHANGE THIS for month*/
				AND YEAR(ACTLSHIP) = {1}		/*CHANGE THIS for year*/
				AND SOPNUMBE like 'I%') header
		/*
			This JOIN with a SELECT subquery gets
			all line items from SOP30300 that have been
			sold using COGS 0-004-4060-000 whose COGS
			index number is 137.
			It then inner JOINs the lineItems with their
			respective headers, but since it is an inner
			join, only header items that are in the line
			items table are returned.
		*/
	JOIN
		(SELECT *
			FROM METRO.dbo.SOP30300
			WHERE CSLSINDX = 137
		) lineItems
		on
			lineItems.SOPNUMBE = header.SOPNUMBE

		/*
			This select gets all of the CCodes (ITMSHNAM) for stock
			items. If this is null, the query will use the
			part number from the lineItem ITMNMBR. It is joined
			by ITEMNMBR.
		*/
	LEFT JOIN 
		(SELECT 
			cogs.ITEMNMBR,
			cogs.ITMSHNAM
			FROM METRO.dbo.IV00101 cogs
			WHERE
				cogs.IVCOGSIX = 137
		) gpItems
		on gpItems.ITEMNMBR = lineItems.ITEMNMBR

		/*
			JOIN the serial number table which has the 
			comma delimited serial numbers
		*/
	LEFT JOIN METRO.dbo.SOP10202 serialTable
		ON	lineItems.LNITMSEQ = serialTable.LNITMSEQ
			AND lineItems.SOPNUMBE = serialTable.SOPNUMBE

		/*
			JOIN the customer master table to get a
			customers account information
		*/
	LEFT JOIN METRO.dbo.RM00101 custMaster
		ON	custMaster.CUSTNMBR = header.CUSTNMBR

		/*
			JOIN the enVision numbers to the left most table
		*/
	LEFT JOIN 
		(SELECT 
			envisionNumberTable.Field_ID,
			envisionNumberTable.Extender_Record_ID,
			envisionNumberTable.STRGA255 AS [ResellerNo],
			customerNameTable.Extender_Key_Values_1 AS [CUSTNAME]
			FROM METRO.dbo.EXT01101 envisionNumberTable
			JOIN METRO.dbo.EXT01100 customerNameTable
			ON customerNameTable.Extender_Record_ID = envisionNumberTable.Extender_Record_ID
			WHERE
				envisionNumberTable.Field_ID = 240
		) enVisionNumber
		ON enVisionNumber.CUSTNAME = header.CUSTNAME

		/*
			Filter out any rows that have the ITMSHNAM of
			TM or Rebate, but include NULL ITMSHNAM rows.
			NULL ITMSHNAM values are non-stock items
		*/
		WHERE
		  (gpItems.ITMSHNAM NOT IN ('TM', 'Rebate')
		  OR gpItems.ITMSHNAM IS NULL)
		  AND lineItems.QTYFULFI <> 0

/*
	Order the report alphabetically by customer name and
	by invoice date
*/
ORDER BY custMaster.CUSTNAME, header.SOPNUMBE