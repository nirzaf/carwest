USE [pos2]
GO
/****** Object:  Table [dbo].[warrentyGRN]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[warrentyGRN](
	[invoiceid] [varchar](50) NULL,
	[warrentycode] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[warrenty]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[warrenty](
	[invoiceid] [varchar](50) NULL,
	[warrentycode] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[vehiclePending]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[vehiclePending](
	[invoiceID] [varchar](50) NOT NULL,
	[vehicleno] [varchar](50) NULL,
	[descrip] [varchar](150) NULL,
	[meterNOw] [varchar](50) NULL,
	[meterNext] [varchar](50) NULL,
	[customerID] [varchar](50) NULL,
	[date] [date] NULL,
 CONSTRAINT [PK_vehiddcle] PRIMARY KEY CLUSTERED 
(
	[invoiceID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[vehicle]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[vehicle](
	[invoiceID] [int] NOT NULL,
	[vehicleno] [varchar](50) NULL,
	[descrip] [varchar](150) NULL,
	[meterNOw] [varchar](50) NULL,
	[meterNext] [varchar](50) NULL,
	[customerID] [varchar](50) NULL,
	[date] [date] NULL,
 CONSTRAINT [PK_vehicle] PRIMARY KEY CLUSTERED 
(
	[invoiceID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[vehcileProfile]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[vehcileProfile](
	[vehicleNo] [varchar](50) NULL,
	[description] [varchar](50) NULL,
	[model] [varchar](50) NULL,
	[brand] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[users]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[users](
	[userName] [varchar](50) NULL,
	[password] [varchar](50) NULL,
	[isCompany] [bit] NULL,
	[itemAdd] [bit] NULL,
	[itemEdit] [bit] NULL,
	[itemDelete] [bit] NULL,
	[customerADD] [bit] NULL,
	[customerEdit] [bit] NULL,
	[customerDelete] [bit] NULL,
	[supplierNew] [bit] NULL,
	[supplierEDIT] [bit] NULL,
	[supplierDelete] [bit] NULL,
	[invoiceNew] [bit] NULL,
	[invoiceEdit] [bit] NULL,
	[invoiceDelete] [bit] NULL,
	[invoiceSerach] [bit] NULL,
	[grnNew] [bit] NULL,
	[grnEdit] [bit] NULL,
	[grnDelete] [bit] NULL,
	[grnSearch] [bit] NULL,
	[returnInvoice] [bit] NULL,
	[returnGRn] [bit] NULL,
	[stock] [bit] NULL,
	[usres] [bit] NULL,
	[payInvoice] [bit] NULL,
	[barcode] [bit] NULL,
	[chequeDeposit] [bit] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[timeSheet]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[timeSheet](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[empId] [varchar](50) NOT NULL,
	[inTime] [time](7) NULL,
	[outTime] [time](7) NULL,
	[date] [date] NOT NULL,
	[lateMIn] [time](7) NULL,
	[otMIn] [time](7) NULL,
	[workMin] [time](7) NULL,
	[inTime2] [time](7) NULL,
	[outTime2] [time](7) NULL,
	[inTime3] [time](7) NULL,
	[outTime3] [time](7) NULL,
 CONSTRAINT [PK_timeSheet2] PRIMARY KEY CLUSTERED 
(
	[id] ASC,
	[empId] ASC,
	[date] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TimeBasedAttandance]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[TimeBasedAttandance](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[inTime] [time](7) NULL,
	[outTime] [time](7) NULL,
	[maxLateMin] [time](7) NULL,
	[maxOtMin] [time](7) NULL,
	[basic] [float] NULL,
	[otPay] [bit] NULL,
	[lateDeduction] [bit] NULL,
	[otRate] [float] NULL,
	[lateRate] [float] NULL,
	[empid] [varchar](50) NULL,
	[WORKINGdAYS] [int] NULL,
	[offDayRate] [float] NULL,
	[lateBalance] [bit] NULL,
	[lunch] [bit] NULL,
	[dayOffInTime] [time](7) NULL,
	[dayOffOutTime] [time](7) NULL,
	[dayOffMaxLate] [time](7) NULL,
	[dayOffMaxOt] [time](7) NULL,
	[dayOffInTime2] [time](7) NULL,
	[dayOffOutTime2] [time](7) NULL,
	[dayOffMaxLate2] [time](7) NULL,
	[dayOffMaxOt2] [time](7) NULL,
 CONSTRAINT [PK_TimeBasedAttandance] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tets2]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tets2](
	[a] [varchar](50) NULL,
	[b] [varchar](50) NULL,
	[c] [varchar](50) NULL,
	[d] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tets]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tets](
	[a] [varchar](50) NULL,
	[b] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[test4]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[test4](
	[test4] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_test4] PRIMARY KEY CLUSTERED 
(
	[test4] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SUPPLIERStatement]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[SUPPLIERStatement](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[reason] [varchar](50) NULL,
	[memo] [varchar](500) NULL,
	[credit] [float] NULL,
	[debit] [float] NULL,
	[states] [bit] NULL,
	[date] [date] NULL,
	[customerID] [varchar](50) NULL,
 CONSTRAINT [PK_customerStatGement] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[supplierBak]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[supplierBak](
	[id] [varchar](50) NOT NULL,
	[name] [varchar](50) NULL,
	[company] [varchar](50) NULL,
	[address] [varchar](500) NULL,
	[mobileNo] [varchar](50) NULL,
	[landNo] [varchar](50) NULL,
	[description] [varchar](800) NULL,
	[email] [varchar](50) NULL,
	[faxNumber] [varchar](50) NULL,
	[auto] [int] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[supplier]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[supplier](
	[id] [varchar](50) NOT NULL,
	[name] [varchar](50) NULL,
	[company] [varchar](50) NULL,
	[address] [varchar](500) NULL,
	[mobileNo] [varchar](50) NULL,
	[landNo] [varchar](50) NULL,
	[description] [varchar](800) NULL,
	[email] [varchar](50) NULL,
	[faxNumber] [varchar](50) NULL,
	[auto] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[subAccounts]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[subAccounts](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](150) NULL,
 CONSTRAINT [PK_subAccounts] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[stockremindercheck2]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[stockremindercheck2](
	[stockReminderCheck2] [varchar](50) NOT NULL,
	[brand] [varchar](50) NULL,
	[qty] [varchar](50) NULL,
 CONSTRAINT [PK_stockremindercheck2] PRIMARY KEY CLUSTERED 
(
	[stockReminderCheck2] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[stockReminderCheck]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[stockReminderCheck](
	[check1] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[stockAlertSetting]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[stockAlertSetting](
	[qty] [int] NULL,
	[notificatons] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[staff]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[staff](
	[id] [varchar](50) NULL,
	[name] [varchar](50) NULL,
	[address] [varchar](250) NULL,
	[mobile] [varchar](50) NULL,
	[salary] [float] NULL,
	[bankingAmount] [float] NULL,
	[epf] [float] NULL,
	[etf] [float] NULL,
	[detail] [varchar](250) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[serviceBy]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[serviceBy](
	[invoiceId] [varchar](50) NULL,
	[serviceBy] [varchar](500) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[salesRef]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[salesRef](
	[id] [varchar](50) NOT NULL,
	[name] [varchar](50) NULL,
	[address] [varchar](500) NULL,
	[mobileNo] [varchar](50) NULL,
	[description] [varchar](800) NULL,
 CONSTRAINT [PK_SaleRef] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[sale]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[sale](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[invoiceID] [varchar](50) NULL,
	[customerID] [varchar](50) NULL,
	[refCode] [varchar](50) NULL,
	[amount] [float] NULL,
	[date] [date] NULL,
 CONSTRAINT [PK_sale] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[salaryPay]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[salaryPay](
	[empid] [varchar](50) NULL,
	[amount] [float] NULL,
	[date] [date] NULL,
	[month] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[salaryLock]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[salaryLock](
	[month] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[returnGoods]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[returnGoods](
	[itemCode] [varchar](50) NULL,
	[qty] [float] NULL,
	[customer] [varchar](50) NULL,
	[credit] [bit] NULL,
	[amount] [float] NULL,
	[datetime] [datetime] NULL,
	[returnID] [varchar](50) NULL,
	[invoiceID] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[returnChequePayment]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[returnChequePayment](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[tempid] [varchar](50) NULL,
	[customer] [varchar](50) NULL,
	[amount] [float] NULL,
	[date] [date] NULL,
	[ref] [varchar](50) NULL,
 CONSTRAINT [PK_returnChequePaymensst] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[returnCheck]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[returnCheck](
	[chequeNumber] [varchar](50) NULL,
	[cheQueCodeNumber] [varchar](50) NULL,
	[cheQueDate] [date] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[receipt2]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[receipt2](
	[id] [int] NOT NULL,
	[date] [date] NULL,
	[ref] [varchar](50) NULL,
	[customer] [varchar](50) NULL,
	[amount] [varchar](500) NULL,
	[amount2] [float] NULL,
	[reason] [varchar](500) NULL,
	[remark] [varchar](500) NULL,
	[term] [varchar](50) NULL,
	[userID] [varchar](50) NULL,
 CONSTRAINT [PK_receipt2] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[receipt]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[receipt](
	[id] [int] NOT NULL,
	[date] [date] NULL,
	[ref] [varchar](50) NULL,
	[customer] [varchar](50) NULL,
	[amount] [varchar](500) NULL,
	[amount2] [float] NULL,
	[reason] [varchar](500) NULL,
	[remark] [varchar](500) NULL,
	[term] [varchar](50) NULL,
	[userID] [varchar](50) NULL,
	[dateEnter] [datetime] NULL,
 CONSTRAINT [PK_receipt] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[rate]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[rate](
	[name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_rate] PRIMARY KEY CLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[quTDetail]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[quTDetail](
	[invoiceID] [int] NOT NULL,
	[itemCode] [varchar](50) NOT NULL,
	[qty] [float] NULL,
	[unitPrice] [float] NULL,
	[totalPrice] [float] NULL,
	[desc] [float] NULL
) ON [PRIMARY]
SET ANSI_PADDING ON
ALTER TABLE [dbo].[quTDetail] ADD [decription] [varchar](500) NULL
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[qua]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[qua](
	[id] [int] NOT NULL,
	[customerID] [varchar](50) NULL,
	[subTotal] [float] NULL,
	[date] [date] NULL,
	[expireDate] [date] NULL,
	[discount] [float] NULL,
	[netTotal] [float] NULL
) ON [PRIMARY]
SET ANSI_PADDING ON
ALTER TABLE [dbo].[qua] ADD [userid] [varchar](500) NULL
ALTER TABLE [dbo].[qua] ADD  CONSTRAINT [PK_qua] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[qu]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[qu](
	[id] [int] NOT NULL,
	[customerID] [varchar](50) NULL,
	[retail] [bit] NULL,
	[subTotal] [float] NULL,
	[date] [date] NULL,
	[goverment] [bit] NULL,
	[PAYTYPE] [varchar](50) NULL,
	[time] [time](7) NULL,
	[discount] [float] NULL,
	[netTotal] [float] NULL,
	[profit] [float] NULL
) ON [PRIMARY]
SET ANSI_PADDING ON
ALTER TABLE [dbo].[qu] ADD [userid] [varchar](500) NULL
ALTER TABLE [dbo].[qu] ADD [cash] [float] NULL
ALTER TABLE [dbo].[qu] ADD [balance] [float] NULL
ALTER TABLE [dbo].[qu] ADD [poNo] [varchar](50) NULL
ALTER TABLE [dbo].[qu] ADD [dateExpire] [date] NULL
ALTER TABLE [dbo].[qu] ADD  CONSTRAINT [PK_qu] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[purchOrderDetail]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[purchOrderDetail](
	[quatId] [int] NOT NULL,
	[itemCode] [varchar](50) NOT NULL,
	[descr] [varchar](500) NOT NULL,
	[purchasingPrice] [float] NOT NULL,
	[qty] [float] NULL,
	[uom] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[purchOrder]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[purchOrder](
	[id] [int] NOT NULL,
	[supplierID] [varchar](50) NULL,
	[date] [date] NULL,
 CONSTRAINT [PK_purch] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[purchasingPriceList]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[purchasingPriceList](
	[code] [varchar](50) NOT NULL,
	[purchasingPrice] [float] NOT NULL,
	[minRetailPrice] [float] NULL,
	[maxRetailPrice] [float] NULL,
	[qty] [float] NULL,
	[date] [datetime] NULL,
 CONSTRAINT [PK_purchasingPriceList] PRIMARY KEY CLUSTERED 
(
	[code] ASC,
	[purchasingPrice] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[processDate]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[processDate](
	[id] [varchar](50) NOT NULL,
	[date] [datetime] NULL,
 CONSTRAINT [PK_processDate] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[plRates]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[plRates](
	[electricity] [float] NULL,
	[water] [float] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[pendingDetail]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[pendingDetail](
	[invoiceID] [int] NOT NULL,
	[itemCode] [varchar](50) NOT NULL,
	[qty] [float] NULL,
	[unitPrice] [float] NULL,
	[totalPrice] [float] NULL,
	[profit] [float] NULL,
	[purchasingPrice] [float] NULL,
	[desc] [float] NULL,
	[pc] [bit] NULL,
	[warrentycode] [varchar](50) NULL,
	[decription] [varchar](500) NULL,
	[uom] [varchar](50) NULL,
	[qtyFull] [float] NULL,
	[typeQ] [int] NULL,
	[t] [varchar](50) NULL,
	[t2] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[pending]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[pending](
	[id] [int] NOT NULL,
	[customerID] [varchar](50) NULL,
	[retail] [bit] NULL,
	[subTotal] [float] NULL,
	[date] [date] NULL,
	[goverment] [bit] NULL,
	[PAYTYPE] [varchar](50) NULL,
	[time] [time](7) NULL,
	[discount] [float] NULL,
	[netTotal] [float] NULL,
	[profit] [float] NULL
) ON [PRIMARY]
SET ANSI_PADDING ON
ALTER TABLE [dbo].[pending] ADD [userid] [varchar](500) NULL
ALTER TABLE [dbo].[pending] ADD [cash] [float] NULL
ALTER TABLE [dbo].[pending] ADD [balance] [float] NULL
ALTER TABLE [dbo].[pending] ADD [poNo] [varchar](50) NULL
ALTER TABLE [dbo].[pending] ADD  CONSTRAINT [PK_invoifceRetail] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[pcItemTep]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[pcItemTep](
	[itemid] [varchar](50) NULL,
	[qty] [float] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[pcIteminvoiceTemp]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[pcIteminvoiceTemp](
	[pcid] [varchar](50) NOT NULL,
	[itemid] [varchar](50) NOT NULL,
	[qty] [float] NULL,
	[warrentycode] [varchar](50) NULL,
 CONSTRAINT [PK_pcIteminvoiceTemp] PRIMARY KEY CLUSTERED 
(
	[pcid] ASC,
	[itemid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[pcIteminvoiceDEtail2]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[pcIteminvoiceDEtail2](
	[invoiceID] [varchar](50) NOT NULL,
	[pcid] [varchar](50) NOT NULL,
	[itemid] [varchar](50) NOT NULL,
	[qty] [float] NULL,
	[purchasingPrice] [float] NULL,
	[type] [int] NULL,
	[id] [int] NULL,
	[warrentycode] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[pcIteminvoiceDEtail]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[pcIteminvoiceDEtail](
	[invoiceID] [varchar](50) NOT NULL,
	[pcid] [varchar](50) NOT NULL,
	[itemid] [varchar](50) NOT NULL,
	[qty] [float] NULL,
	[purchasingPrice] [float] NULL,
	[type] [int] NULL,
	[id] [int] NULL,
	[warrentycode] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[pcInvoice2]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[pcInvoice2](
	[invoiceId] [varchar](50) NULL,
	[pcid] [varchar](50) NULL,
	[qty] [float] NULL,
	[id] [int] NULL,
	[warrentycode] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[pcInvoice]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[pcInvoice](
	[invoiceId] [varchar](50) NULL,
	[pcid] [varchar](50) NULL,
	[qty] [float] NULL,
	[id] [int] NULL,
	[warrentycode] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[pcDetailTemp]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[pcDetailTemp](
	[id] [varchar](50) NOT NULL,
	[itemId] [varchar](50) NOT NULL,
	[qty] [float] NULL,
	[warrenty] [int] NULL,
 CONSTRAINT [PK_pcDetailTemp] PRIMARY KEY CLUSTERED 
(
	[id] ASC,
	[itemId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[pcDetail]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[pcDetail](
	[id] [varchar](50) NOT NULL,
	[itemId] [varchar](50) NOT NULL,
	[qty] [float] NULL,
	[warrenty] [int] NULL,
 CONSTRAINT [PK_pcDetail] PRIMARY KEY CLUSTERED 
(
	[id] ASC,
	[itemId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[pc]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[pc](
	[id] [varchar](50) NULL,
	[description] [varchar](150) NULL,
	[brand] [varchar](50) NULL,
	[retailPrice] [float] NULL,
	[wholeSalePrice] [float] NULL,
	[qty] [float] NULL,
	[warrenty] [int] NULL,
	[name] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[paySheetAudit]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[paySheetAudit](
	[empId] [varchar](50) NULL,
	[month] [varchar](50) NULL,
	[epfNO] [varchar](50) NULL,
	[basic] [float] NULL,
	[br] [float] NULL,
	[livingAllowance] [float] NULL,
	[travellingAllowance] [float] NULL,
	[attendanceAllowance] [float] NULL,
	[mealAllowance] [float] NULL,
	[noPay] [float] NULL,
	[grossSalary] [float] NULL,
	[totEpf] [float] NULL,
	[epf12] [float] NULL,
	[etf3] [float] NULL,
	[companyCommitment] [float] NULL,
	[salaryAdvance] [float] NULL,
	[loan] [float] NULL,
	[epf8] [float] NULL,
	[paye] [float] NULL,
	[totalDeduction] [float] NULL,
	[netPay] [float] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[paysheet]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[paysheet](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[empID] [varchar](50) NULL,
	[month] [varchar](50) NULL,
	[workingDays] [int] NULL,
	[absentDays] [int] NULL,
	[Basic] [float] NULL,
	[bankingAmount] [float] NULL,
	[fixedAllownces] [float] NULL,
	[meal] [float] NULL,
	[allwonces] [float] NULL,
	[gross] [float] NULL,
	[bonus] [float] NULL,
	[bf] [float] NULL,
	[totEaring] [float] NULL,
	[epf8] [float] NULL,
	[punishD] [float] NULL,
	[loan] [float] NULL,
	[advanced] [float] NULL,
	[totalDeductions] [float] NULL,
	[net] [float] NULL,
	[pay] [float] NULL,
	[balance] [float] NULL,
	[commise] [float] NULL,
	[epf12] [float] NOT NULL,
	[etf3] [float] NOT NULL,
 CONSTRAINT [PK_paysheet] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[paidInvoice]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[paidInvoice](
	[id] [varchar](50) NULL,
	[subTotal] [float] NULL,
	[paid] [float] NULL,
	[balance] [float] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[overPayS]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[overPayS](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[customer] [varchar](50) NULL,
	[amount] [float] NULL,
	[tempID] [varchar](50) NULL,
	[invoiceID] [varchar](50) NOT NULL,
 CONSTRAINT [PK_overPayS] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[overPayC]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[overPayC](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[customer] [varchar](50) NULL,
	[amount] [float] NULL,
	[tempID] [varchar](50) NULL,
	[invoiceID] [varchar](50) NOT NULL,
 CONSTRAINT [PK_overPayC] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[openingBalance]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[openingBalance](
	[userID] [varchar](50) NOT NULL,
	[amount] [float] NULL,
	[date] [date] NOT NULL,
 CONSTRAINT [PK_openingBalance] PRIMARY KEY CLUSTERED 
(
	[userID] ASC,
	[date] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[nopay]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[nopay](
	[empid] [varchar](50) NOT NULL,
	[date] [date] NOT NULL,
	[reason] [varchar](100) NULL,
 CONSTRAINT [PK_nopay] PRIMARY KEY CLUSTERED 
(
	[empid] ASC,
	[date] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[meal]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[meal](
	[empid] [int] NULL,
	[amount] [float] NULL,
	[month] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[loginCheck]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[loginCheck](
	[name] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[login]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[login](
	[id] [int] NOT NULL,
	[type] [varchar](50) NULL,
	[name] [varchar](50) NULL,
	[password] [varchar](50) NULL,
 CONSTRAINT [PK_login] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[loanHistory]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[loanHistory](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[empid] [varchar](50) NULL,
	[month] [varchar](50) NULL,
	[amount] [float] NULL,
	[loanID] [int] NULL,
 CONSTRAINT [PK_loanHistory] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[loan_]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[loan_](
	[empid] [int] NULL,
	[amount] [float] NULL,
	[installment] [int] NULL,
	[month] [varchar](50) NULL,
	[date] [date] NULL,
	[type] [varchar](50) NULL,
	[paid] [float] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[loan]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[loan](
	[empid] [varchar](50) NULL,
	[amount] [float] NULL,
	[installment] [int] NULL,
	[month] [varchar](50) NULL,
	[date] [date] NULL,
	[type] [varchar](50) NULL,
	[paid] [float] NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_loan2] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[liabilitiesAccountStatment]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[liabilitiesAccountStatment](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[accountid] [int] NULL,
	[number] [varchar](50) NULL,
	[type] [varchar](50) NULL,
	[payee] [varchar](100) NULL,
	[memo] [varchar](max) NULL,
	[isDeposit] [bit] NULL,
	[credit] [bit] NULL,
	[date] [date] NULL,
	[amount] [float] NULL,
	[accountFor] [varchar](50) NULL,
	[statementID] [varchar](50) NULL,
 CONSTRAINT [PK_liabilitiesAccountStatmentCard] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[liabilities]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[liabilities](
	[id] [int] NOT NULL,
	[name] [varchar](50) NULL,
	[openingBalance] [float] NULL,
	[openingDate] [date] NULL,
 CONSTRAINT [PK_liabilities] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[leave]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[leave](
	[empid] [varchar](50) NOT NULL,
	[date] [date] NOT NULL,
	[states] [bit] NULL,
 CONSTRAINT [PK_leave] PRIMARY KEY CLUSTERED 
(
	[empid] ASC,
	[date] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[itemTemp2]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[itemTemp2](
	[CODE] [varchar](50) NULL,
	[CATEGORY] [varchar](50) NULL,
	[BRAND] [varchar](50) NULL,
	[DETAIL] [varchar](50) NULL,
	[QTY] [float] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[itemTemp_]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[itemTemp_](
	[code] [varchar](50) NULL,
	[categodry] [varchar](50) NULL,
	[brand] [varchar](50) NULL,
	[des] [varchar](50) NULL,
	[des2] [varchar](50) NULL,
	[qty] [float] NULL,
	[cost] [float] NULL,
	[retail] [float] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[itemTemp]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[itemTemp](
	[name] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[itemSub]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[itemSub](
	[code] [varchar](50) NULL,
	[count] [int] NULL,
	[Tprice] [float] NULL,
	[uPrice] [float] NULL,
	[rate] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[itemStatementStock]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[itemStatementStock](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[itemCode] [varchar](50) NULL,
	[qty] [float] NULL,
	[date] [date] NULL,
	[type] [varchar](50) NULL,
 CONSTRAINT [PK_itemStatemeIInt] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[itemStatement]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[itemStatement](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[invoiceID] [varchar](50) NULL,
	[itemCode] [varchar](50) NULL,
	[credit] [bit] NULL,
	[qty] [float] NULL,
	[date] [date] NULL,
	[type] [varchar](50) NULL,
	[user] [varchar](50) NULL,
	[purchsingPrice] [float] NOT NULL,
 CONSTRAINT [PK_itemStatement] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ITEMFEED]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ITEMFEED](
	[ID] [varchar](50) NULL,
	[ID2] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[itemA]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[itemA](
	[code] [varchar](50) NOT NULL,
	[brand] [varchar](50) NULL,
	[categorey] [varchar](50) NULL,
	[description] [varchar](50) NULL,
	[remark] [varchar](50) NULL,
	[rate] [varchar](50) NULL,
	[purchasingPrice] [float] NULL,
	[retailPrice] [float] NULL,
	[billingPrice] [float] NULL,
	[qty] [float] NULL,
	[warrentyCode] [varchar](50) NULL,
	[detail] [varchar](max) NULL,
 CONSTRAINT [PK_item2] PRIMARY KEY CLUSTERED 
(
	[code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[item4]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[item4](
	[code] [varchar](50) NULL,
	[description] [varchar](max) NULL,
	[rate] [varchar](50) NULL,
	[retailPrice] [float] NULL,
	[wholeSalePrice] [float] NULL,
	[custoemPrice] [int] NULL,
	[upadtedDate] [datetime] NULL,
	[qty] [float] NULL,
	[brand] [varchar](50) NULL,
	[category1] [varchar](50) NULL,
	[category2] [varchar](50) NULL,
	[category3] [varchar](50) NULL,
	[itemName] [varchar](max) NULL,
	[purchasingPrice] [float] NULL,
	[discount] [float] NULL,
	[discount2] [float] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[item20200801_]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[item20200801_](
	[code] [varchar](50) NULL,
	[category] [varchar](50) NULL,
	[brand] [varchar](50) NULL,
	[description] [varchar](50) NULL,
	[qty] [float] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[item20200801]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[item20200801](
	[code] [varchar](50) NULL,
	[category] [varchar](50) NULL,
	[brand] [varchar](50) NULL,
	[description] [varchar](50) NULL,
	[qty] [float] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[item2]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[item2](
	[code] [varchar](50) NOT NULL,
	[brand] [varchar](50) NULL,
	[categorey] [varchar](50) NULL,
	[description] [varchar](50) NULL,
	[remark] [varchar](50) NULL,
	[rate] [varchar](50) NULL,
	[purchasingPrice] [float] NULL,
	[retailPrice] [float] NULL,
	[billingPrice] [float] NULL,
	[qty] [float] NULL,
	[warrentyCode] [varchar](50) NULL,
	[detail] [varchar](50) NULL,
 CONSTRAINT [PK_item] PRIMARY KEY CLUSTERED 
(
	[code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[item]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[item](
	[code] [varchar](50) NOT NULL,
	[brand] [varchar](50) NULL,
	[categorey] [varchar](50) NULL,
	[description] [varchar](max) NULL,
	[remark] [varchar](50) NULL,
	[rate] [varchar](50) NULL,
	[purchasingPrice] [float] NULL,
	[retailPrice] [float] NULL,
	[billingPrice] [float] NULL,
	[qty] [float] NULL,
	[warrentyCode] [varchar](50) NULL,
	[detail] [varchar](max) NULL,
	[id] [int] NOT NULL,
	[isItem] [bit] NOT NULL,
	[sparePart] [bit] NOT NULL,
 CONSTRAINT [PK_item3] PRIMARY KEY CLUSTERED 
(
	[code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[invoiceType]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[invoiceType](
	[invoiceType] [varchar](50) NOT NULL,
	[customerInvoiceView] [bit] NULL,
	[cashFlowInvoiceView] [bit] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[invoiceTerm]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[invoiceTerm](
	[invoiceid] [varchar](50) NOT NULL,
	[cash] [bit] NULL,
	[credit] [bit] NULL,
	[cheque] [bit] NULL,
	[card] [bit] NULL
) ON [PRIMARY]
SET ANSI_PADDING OFF
ALTER TABLE [dbo].[invoiceTerm] ADD [userID] [varchar](50) NULL
ALTER TABLE [dbo].[invoiceTerm] ADD  CONSTRAINT [PK_invoiceTerm] PRIMARY KEY CLUSTERED 
(
	[invoiceid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[invoiceS]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[invoiceS](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[customerID] [varchar](50) NULL,
	[retail] [bit] NULL,
	[subTotal] [float] NULL,
	[date] [date] NULL,
	[goverment] [bit] NULL,
	[PAYTYPE] [varchar](50) NULL,
	[time] [time](7) NULL,
	[discount] [float] NULL,
	[netTotal] [float] NULL,
	[profit] [float] NULL,
 CONSTRAINT [PK_invoiceS] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[invoiceRetailDetail2]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[invoiceRetailDetail2](
	[invoiceID] [int] NOT NULL,
	[itemCode] [varchar](50) NOT NULL,
	[qty] [float] NULL,
	[unitPrice] [float] NULL,
	[totalPrice] [float] NULL,
	[profit] [float] NULL,
	[purchasingPrice] [float] NULL,
	[desc] [float] NULL,
	[pc] [bit] NULL
) ON [PRIMARY]
SET ANSI_PADDING ON
ALTER TABLE [dbo].[invoiceRetailDetail2] ADD [warrentycode] [varchar](50) NULL
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[invoiceRetailDetail_bak]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[invoiceRetailDetail_bak](
	[invoiceID] [int] NOT NULL,
	[itemCode] [varchar](50) NOT NULL,
	[qty] [float] NULL,
	[unitPrice] [float] NULL,
	[totalPrice] [float] NULL,
	[profit] [float] NULL,
	[purchasingPrice] [float] NULL,
	[desc] [float] NULL,
	[pc] [bit] NULL,
	[warrentycode] [varchar](50) NULL,
	[decription] [varchar](500) NULL,
	[uom] [varchar](50) NULL,
	[qtyFull] [float] NULL,
	[typeQ] [int] NULL,
 CONSTRAINT [PK_invoiceRetailDetail] PRIMARY KEY CLUSTERED 
(
	[invoiceID] ASC,
	[itemCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[invoiceRetailDetail]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[invoiceRetailDetail](
	[invoiceID] [int] NOT NULL,
	[itemCode] [varchar](50) NOT NULL,
	[qty] [float] NULL,
	[unitPrice] [float] NULL,
	[totalPrice] [float] NULL,
	[profit] [float] NULL,
	[purchasingPrice] [float] NULL,
	[desc] [float] NULL,
	[pc] [bit] NULL,
	[warrentycode] [varchar](50) NULL,
	[decription] [varchar](500) NULL,
	[uom] [varchar](50) NULL,
	[qtyFull] [float] NULL,
	[typeQ] [int] NULL,
	[t] [varchar](50) NULL,
	[t2] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[invoiceRetail]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[invoiceRetail](
	[id] [int] NOT NULL,
	[customerID] [varchar](50) NULL,
	[retail] [bit] NULL,
	[subTotal] [float] NULL,
	[date] [date] NULL,
	[goverment] [bit] NULL,
	[PAYTYPE] [varchar](50) NULL,
	[time] [time](7) NULL,
	[discount] [float] NULL,
	[netTotal] [float] NULL,
	[profit] [float] NULL
) ON [PRIMARY]
SET ANSI_PADDING ON
ALTER TABLE [dbo].[invoiceRetail] ADD [userid] [varchar](500) NULL
ALTER TABLE [dbo].[invoiceRetail] ADD [cash] [float] NULL
ALTER TABLE [dbo].[invoiceRetail] ADD [balance] [float] NULL
ALTER TABLE [dbo].[invoiceRetail] ADD [poNo] [varchar](50) NULL
ALTER TABLE [dbo].[invoiceRetail] ADD  CONSTRAINT [PK_invoiceRetail] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[invoiceDump]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[invoiceDump](
	[id] [int] NOT NULL,
	[customerID] [varchar](50) NULL,
	[retail] [bit] NULL,
	[subTotal] [float] NULL,
	[date] [date] NULL,
	[goverment] [bit] NULL,
	[PAYTYPE] [varchar](50) NULL,
	[time] [time](7) NULL,
	[discount] [float] NULL,
	[netTotal] [float] NULL,
	[profit] [float] NULL,
	[userid] [varchar](500) NULL,
	[cash] [float] NULL,
	[balance] [float] NULL,
	[poNo] [int] NOT NULL,
	[dumpno] [int] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[invoiceCreditPaid]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[invoiceCreditPaid](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[invoiceID] [int] NULL,
	[amount] [float] NULL,
	[paid] [float] NULL,
	[balance] [float] NULL,
	[userid] [varchar](50) NULL,
	[date] [date] NULL,
	[time] [time](7) NULL
) ON [PRIMARY]
SET ANSI_PADDING OFF
ALTER TABLE [dbo].[invoiceCreditPaid] ADD [tempID] [varchar](50) NULL
ALTER TABLE [dbo].[invoiceCreditPaid] ADD  CONSTRAINT [PK_test4dd] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[incomeAccountStatement]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[incomeAccountStatement](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[invoiceID] [varchar](50) NULL,
	[customerID] [varchar](50) NULL,
	[accountID] [varchar](50) NULL,
	[amount] [float] NULL,
	[date] [date] NULL,
	[accountFor] [varchar](50) NULL,
	[memo] [varchar](150) NULL,
	[statementID] [varchar](50) NULL,
 CONSTRAINT [PK_invoiceAccountStatement2] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[incomeAccounts]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[incomeAccounts](
	[id] [int] NOT NULL,
	[name] [varchar](50) NULL,
	[subAcount] [int] NULL,
 CONSTRAINT [PK_incomeAccounts] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[im]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[im](
	[id] [varchar](50) NULL,
	[image] [image] NULL,
	[price] [varchar](50) NULL,
	[code] [varchar](50) NULL,
	[name] [varchar](max) NULL,
	[codeItem] [varchar](50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[grnTerm]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[grnTerm](
	[invoiceid] [varchar](50) NOT NULL,
	[cash] [bit] NULL,
	[credit] [bit] NULL,
	[cheque] [bit] NULL,
	[card] [bit] NULL
) ON [PRIMARY]
SET ANSI_PADDING OFF
ALTER TABLE [dbo].[grnTerm] ADD [userID] [varchar](50) NULL
ALTER TABLE [dbo].[grnTerm] ADD  CONSTRAINT [PK_iffnvoiceTerm] PRIMARY KEY CLUSTERED 
(
	[invoiceid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[grnDetaila]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[grnDetaila](
	[grnId] [int] NOT NULL,
	[itemCode] [varchar](50) NOT NULL,
	[purchasingPrice] [float] NOT NULL,
	[retailPrice] [float] NULL,
	[wholeSalePrice] [float] NULL,
	[qty] [float] NULL,
	[totalPrice] [float] NULL,
	[type] [bit] NULL,
	[descriotion] [varchar](50) NULL,
	[warrentyCode] [varchar](max) NULL,
	[disc] [float] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[grnDetail]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[grnDetail](
	[invoiceID] [int] NOT NULL,
	[itemCode] [varchar](50) NOT NULL,
	[qty] [float] NULL,
	[unitPrice] [float] NULL,
	[totalPrice] [float] NULL,
	[profit] [float] NULL,
	[purchasingPrice] [float] NULL,
	[desc] [float] NULL,
	[pc] [bit] NULL,
	[warrentycode] [varchar](50) NULL,
	[decription] [varchar](500) NULL,
	[uom] [varchar](50) NULL,
	[qtyFull] [float] NULL,
	[typeQ] [int] NULL,
	[qtyOut] [float] NOT NULL,
	[isQtyIn] [bit] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[grnCreditPaid]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[grnCreditPaid](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[invoiceID] [int] NULL,
	[amount] [float] NULL,
	[paid] [float] NULL,
	[balance] [float] NULL,
	[userid] [varchar](50) NULL,
	[date] [date] NULL,
	[time] [time](7) NULL,
	[tempID] [varchar](50) NULL,
 CONSTRAINT [PK_thesgft4dd] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GRN]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[GRN](
	[id] [int] NOT NULL,
	[customerID] [varchar](50) NULL,
	[retail] [bit] NULL,
	[subTotal] [float] NULL,
	[date] [date] NULL,
	[goverment] [bit] NULL,
	[PAYTYPE] [varchar](50) NULL,
	[time] [time](7) NULL,
	[discount] [float] NULL,
	[netTotal] [float] NULL,
	[profit] [float] NULL
) ON [PRIMARY]
SET ANSI_PADDING ON
ALTER TABLE [dbo].[GRN] ADD [userid] [varchar](500) NULL
ALTER TABLE [dbo].[GRN] ADD [cash] [float] NULL
ALTER TABLE [dbo].[GRN] ADD [balance] [float] NULL
ALTER TABLE [dbo].[GRN] ADD [poNo] [varchar](50) NULL
ALTER TABLE [dbo].[GRN] ADD [invoiceNo] [varchar](50) NOT NULL
ALTER TABLE [dbo].[GRN] ADD [grnDate] [date] NOT NULL
ALTER TABLE [dbo].[GRN] ADD  CONSTRAINT [PK_invoiceRetDDail] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[fullService]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[fullService](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[invoiceId] [int] NULL,
	[isDf] [bit] NULL,
	[isOf] [bit] NULL,
	[isEo] [bit] NULL,
	[isGo] [bit] NULL,
	[isAf] [bit] NULL,
	[isGrees] [bit] NULL,
	[period] [int] NULL,
	[messageSend] [varchar](max) NOT NULL,
 CONSTRAINT [PK_fullService] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[fixedDeduction]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[fixedDeduction](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[empID] [varchar](50) NULL,
	[name] [varchar](150) NULL,
	[amount] [float] NULL,
 CONSTRAINT [PK_fixedDeduction] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[fixedAssetsAccountStatment]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[fixedAssetsAccountStatment](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[accountid] [int] NULL,
	[number] [varchar](50) NULL,
	[type] [varchar](50) NULL,
	[payee] [varchar](100) NULL,
	[memo] [varchar](max) NULL,
	[isDeposit] [bit] NULL,
	[credit] [bit] NULL,
	[date] [date] NULL,
	[amount] [float] NULL,
	[accountFor] [varchar](50) NULL,
	[statementID] [varchar](50) NULL,
 CONSTRAINT [PK_fixedAssetsAccountStatmentCard] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[fixedAssets]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[fixedAssets](
	[id] [int] NOT NULL,
	[name] [varchar](50) NULL,
	[openingBalance] [float] NULL,
	[openingDate] [date] NULL,
 CONSTRAINT [PK_fixedAssets] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[fixedAllownces]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[fixedAllownces](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[empid] [varchar](50) NULL,
	[name] [varchar](100) NULL,
	[Amount] [float] NULL,
 CONSTRAINT [PK_fixedAllownces] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[expensesAccountStatement]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[expensesAccountStatement](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[invoiceID] [varchar](50) NULL,
	[customerID] [varchar](50) NULL,
	[accountID] [varchar](50) NULL,
	[amount] [float] NULL,
	[date] [date] NULL,
	[accountFor] [varchar](50) NULL,
	[memo] [varchar](150) NULL,
	[statementID] [varchar](50) NULL,
 CONSTRAINT [PK_expensesAccountStatement] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ExpensesAccounts]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ExpensesAccounts](
	[id] [int] NOT NULL,
	[name] [varchar](50) NULL,
	[subAcount] [int] NULL,
 CONSTRAINT [PK_ExpensesAccounts] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[equityAccountStatment]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[equityAccountStatment](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[accountid] [int] NULL,
	[number] [varchar](50) NULL,
	[type] [varchar](50) NULL,
	[payee] [varchar](100) NULL,
	[memo] [varchar](max) NULL,
	[isDeposit] [bit] NULL,
	[credit] [bit] NULL,
	[date] [date] NULL,
	[amount] [float] NULL,
	[accountFor] [varchar](50) NULL,
	[statementID] [varchar](50) NULL,
 CONSTRAINT [PK_equityAccountStatmentCard] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[equity]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[equity](
	[id] [int] NOT NULL,
	[name] [varchar](50) NULL,
	[openingBalance] [float] NULL,
	[openingDate] [date] NULL,
 CONSTRAINT [PK_equity] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Emp]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[Emp](
	[name] [varchar](150) NULL,
	[residentialAddress] [varchar](250) NULL,
	[mobileNUmber] [varchar](50) NULL,
	[empid] [int] NOT NULL,
	[epfBasic] [float] NULL,
	[bankingAmount] [float] NULL,
	[epf] [float] NULL,
	[etf] [float] NULL,
	[type] [varchar](50) NULL,
	[epf12] [float] NULL,
	[incentive] [float] NOT NULL,
	[allowances] [float] NOT NULL,
	[meal] [float] NOT NULL,
	[gross] [float] NOT NULL,
	[resgin] [bit] NOT NULL,
	[bouns] [float] NOT NULL,
	[punish] [float] NOT NULL
) ON [PRIMARY]
SET ANSI_PADDING ON
ALTER TABLE [dbo].[Emp] ADD [holiday] [varchar](100) NOT NULL
ALTER TABLE [dbo].[Emp] ADD [advanced] [float] NOT NULL
ALTER TABLE [dbo].[Emp] ADD [loan] [float] NOT NULL
ALTER TABLE [dbo].[Emp] ADD [nic] [varchar](50) NOT NULL
ALTER TABLE [dbo].[Emp] ADD [isEpf] [bit] NOT NULL
ALTER TABLE [dbo].[Emp] ADD [isExecutive] [bit] NOT NULL
ALTER TABLE [dbo].[Emp] ADD [epfNO] [varchar](50) NOT NULL
ALTER TABLE [dbo].[Emp] ADD  CONSTRAINT [PK_Emmpa] PRIMARY KEY CLUSTERED 
(
	[empid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[earning]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[earning](
	[id] [varchar](50) NULL,
	[reason] [varchar](350) NULL,
	[amount] [float] NULL,
	[month] [varchar](50) NULL,
	[date] [date] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[dumpInvoiceSetting]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[dumpInvoiceSetting](
	[count] [int] NULL,
	[maxAmount] [float] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[dumpInvoiceCount]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[dumpInvoiceCount](
	[date] [date] NOT NULL,
	[count] [int] NOT NULL,
	[id] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[distance]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[distance](
	[code] [varchar](50) NOT NULL,
	[value] [varchar](50) NULL,
 CONSTRAINT [PK_distance] PRIMARY KEY CLUSTERED 
(
	[code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DepreciationAccounts]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DepreciationAccounts](
	[id] [int] NOT NULL,
	[name] [varchar](50) NULL,
	[subAcount] [int] NULL,
 CONSTRAINT [PK_ExpenseddsAccounts] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[deletedInvoice]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[deletedInvoice](
	[invoiceID] [varchar](50) NULL,
	[userID] [varchar](50) NULL,
	[date] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[deleteAttandance]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[deleteAttandance](
	[date] [datetime] NOT NULL,
	[empid] [int] NOT NULL,
 CONSTRAINT [PK_deleteAttandance] PRIMARY KEY CLUSTERED 
(
	[empid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[deduct]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[deduct](
	[id] [varchar](50) NULL,
	[type] [varchar](50) NULL,
	[reason] [varchar](500) NULL,
	[amount] [float] NULL,
	[month] [varchar](50) NULL,
	[date] [datetime] NULL,
	[dateGet] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[dayOff]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[dayOff](
	[empid] [int] NOT NULL,
	[date] [date] NOT NULL,
	[period] [int] NULL,
 CONSTRAINT [PK_dayOff] PRIMARY KEY CLUSTERED 
(
	[empid] ASC,
	[date] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[dayBasedAttandance]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[dayBasedAttandance](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[intime] [time](7) NULL,
	[outtime] [time](7) NULL,
	[maxot] [time](7) NULL,
	[maxlate] [time](7) NULL,
	[daySalary] [float] NULL,
	[otRate] [float] NULL,
	[laterate] [float] NULL,
	[otPay] [bit] NULL,
	[lateDeduction] [bit] NULL,
	[lateBalance] [bit] NULL,
	[lunch] [bit] NULL,
	[empID] [varchar](50) NULL,
	[workingDays] [int] NULL,
	[dayOffInTime] [time](7) NULL,
	[dayOffOutTime] [time](7) NULL,
	[dayOffMaxLate] [time](7) NULL,
	[dayOffMaxOt] [time](7) NULL,
	[dayOffInTime2] [time](7) NULL,
	[dayOffOutTime2] [time](7) NULL,
	[dayOffMaxLate2] [time](7) NULL,
	[dayOffMaxOt2] [time](7) NULL,
 CONSTRAINT [PK_dayBasedAttandance] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[customerStatement]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[customerStatement](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[reason] [varchar](50) NULL,
	[memo] [varchar](500) NULL,
	[credit] [float] NULL,
	[debit] [float] NULL,
	[states] [bit] NULL,
	[date] [date] NULL,
	[customerID] [varchar](50) NULL,
 CONSTRAINT [PK_customerStatement] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[customerBak]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[customerBak](
	[id] [varchar](50) NOT NULL,
	[name] [varchar](50) NULL,
	[company] [varchar](50) NULL,
	[address] [varchar](500) NULL,
	[mobileNo] [varchar](50) NULL,
	[landNo] [varchar](50) NULL,
	[description] [varchar](800) NULL,
	[email] [varchar](50) NULL,
	[faxNumber] [varchar](50) NULL,
	[auto] [int] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[customer]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[customer](
	[id] [varchar](50) NOT NULL,
	[name] [varchar](50) NULL,
	[company] [varchar](50) NULL,
	[address] [varchar](500) NULL,
	[mobileNo] [varchar](50) NULL,
	[landNo] [varchar](50) NULL,
	[description] [varchar](800) NULL,
	[email] [varchar](50) NULL,
	[faxNumber] [varchar](50) NULL,
	[auto] [int] IDENTITY(1,1) NOT NULL,
	[isEpf] [bit] NOT NULL,
	[isExecutive] [bit] NOT NULL,
	[epfNO] [varchar](50) NOT NULL,
	[contact1Name] [varchar](250) NULL,
	[contact1Name2] [varchar](250) NULL,
	[contact1Name3] [varchar](250) NULL,
	[contact2] [varchar](250) NULL,
	[contact3] [varchar](250) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[custom]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[custom](
	[saveInvoiceWithoutPay] [bit] NULL,
	[changeInvoiceDifDate] [bit] NULL,
	[dumpInvoice] [bit] NULL,
	[grnCashFlowAuto] [bit] NULL,
	[discountByPresantge] [bit] NULL,
	[isService] [bit] NULL,
	[clear] [bit] NULL,
	[cashPaidView] [bit] NULL,
	[separte] [bit] NULL,
	[isList] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[custoemPrice]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[custoemPrice](
	[customerID] [varchar](50) NOT NULL,
	[itemID] [varchar](50) NOT NULL,
	[price] [float] NULL,
 CONSTRAINT [PK_custoemPrice] PRIMARY KEY CLUSTERED 
(
	[customerID] ASC,
	[itemID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[creditReminderSetting]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[creditReminderSetting](
	[dateCount] [int] NULL,
	[homeLoading] [bit] NULL,
	[homeLoadCheck] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[creditInvoiceRetailHistorey]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[creditInvoiceRetailHistorey](
	[invoiceID] [varchar](50) NOT NULL,
	[customerId] [varchar](50) NULL,
	[amount] [float] NULL,
	[paid] [float] NULL,
	[balance] [float] NULL,
	[duration] [int] NULL,
	[date] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[creditInvoiceRetailBak]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[creditInvoiceRetailBak](
	[customerId] [varchar](50) NULL,
	[amount] [float] NULL,
	[paid] [float] NULL,
	[balance] [float] NULL,
	[duration] [int] NULL,
	[date] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[creditInvoiceRetail]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[creditInvoiceRetail](
	[invoiceID] [int] NOT NULL,
	[customerId] [varchar](50) NULL,
	[amount] [float] NULL,
	[paid] [float] NULL,
	[balance] [float] NULL,
	[duration] [int] NULL,
	[date] [datetime] NULL,
	[requstDate] [date] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[creditGRNHistorey]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[creditGRNHistorey](
	[grnID] [int] NOT NULL,
	[supplierId] [varchar](50) NULL,
	[amount] [float] NULL,
	[paid] [float] NULL,
	[balance] [float] NULL,
	[duration] [int] NULL,
	[date] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[creditGRNBak]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[creditGRNBak](
	[grnID] [int] NOT NULL,
	[supplierId] [varchar](50) NULL,
	[amount] [float] NULL,
	[paid] [float] NULL,
	[balance] [float] NULL,
	[duration] [int] NULL,
	[date] [datetime] NULL,
 CONSTRAINT [PK_creditGRNBak] PRIMARY KEY CLUSTERED 
(
	[grnID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[creditGRN]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[creditGRN](
	[invoiceID] [int] NOT NULL,
	[customerId] [varchar](50) NULL,
	[amount] [float] NULL,
	[paid] [float] NULL,
	[balance] [float] NULL,
	[duration] [int] NULL,
	[date] [datetime] NULL,
	[requstDate] [date] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[companyInvoice]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[companyInvoice](
	[id] [varchar](50) NOT NULL,
	[companyID] [int] NULL,
	[isVat] [bit] NULL,
	[vatAmount] [float] NULL,
	[isNBT] [bit] NULL,
	[nbtAmount] [float] NULL,
	[subTotal] [float] NULL,
	[date] [date] NULL,
 CONSTRAINT [PK_companyInvoice] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[companyGRN]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[companyGRN](
	[id] [varchar](50) NOT NULL,
	[companyID] [int] NULL,
	[isVat] [bit] NULL,
	[vatAmount] [float] NULL,
	[isNBT] [bit] NULL,
	[nbtAmount] [float] NULL,
	[subTotal] [float] NULL,
	[discount] [float] NULL,
	[date] [date] NULL,
 CONSTRAINT [PK_companyGRN] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[company]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[company](
	[name] [varchar](max) NULL,
	[address] [varchar](max) NULL,
	[telNo] [varchar](50) NULL,
	[faxNo] [varchar](50) NULL,
	[email] [varchar](50) NULL,
	[web] [varchar](50) NULL,
	[regNo] [varchar](50) NULL,
	[isTax] [bit] NULL,
	[taxPre] [float] NULL,
	[isNBT] [bit] NULL,
	[nbtPre] [float] NULL,
	[defa] [bit] NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_companyNew2] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[commis]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[commis](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[empid] [varchar](50) NULL,
	[reason] [varchar](50) NULL,
	[amount] [float] NULL,
	[disc] [float] NULL,
	[amount2] [float] NULL,
	[date] [date] NULL,
 CONSTRAINT [PK_commis] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[chequeSummery]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[chequeSummery](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[bank] [varchar](50) NULL,
	[branch] [varchar](50) NULL,
	[acNO] [varchar](50) NULL,
	[recived] [bit] NULL,
	[deposit] [bit] NULL,
	[send] [bit] NULL,
	[amount] [float] NULL,
	[sendBank] [varchar](50) NULL,
	[chequeNumeber] [varchar](50) NULL,
	[date] [date] NULL,
	[resgin] [bit] NULL,
	[depositC] [bit] NULL,
	[customerID] [varchar](50) NULL,
	[customerID_] [varchar](50) NULL,
	[customerName] [varchar](50) NULL,
	[customerAddress] [varchar](50) NULL,
	[customerMObile] [varchar](50) NULL,
 CONSTRAINT [PK_chequeSummery_] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[chequeReminderSetting]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[chequeReminderSetting](
	[dateCount] [int] NULL,
	[homeLoading] [bit] NULL,
	[homeLoadCheck] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[chequeInvoiceS]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[chequeInvoiceS](
	[invoiceId] [int] NOT NULL,
	[cutomerId] [varchar](50) NULL,
	[amount] [float] NULL,
	[paid] [float] NULL,
	[cheque] [float] NULL,
	[chequenumber] [varchar](50) NULL,
	[chequeDate] [date] NULL,
	[datetime] [datetime] NULL,
	[checkCodeNo] [varchar](1) NULL,
 CONSTRAINT [PK_chequeInvoiceS] PRIMARY KEY CLUSTERED 
(
	[invoiceId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[chequeInvoiceRetailLoad]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[chequeInvoiceRetailLoad](
	[invoiceID] [int] NOT NULL,
	[cutomerId] [varchar](50) NULL,
	[amount] [float] NULL,
	[paid] [float] NULL,
	[cheque] [float] NULL,
	[chequenumber] [varchar](50) NOT NULL,
	[chequeDate] [date] NULL,
	[datetime] [datetime] NULL,
	[checkCodeNo] [varchar](50) NULL,
	[deposit] [bit] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[chequeInvoiceRetailHistorey]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[chequeInvoiceRetailHistorey](
	[invoiceId] [varchar](50) NOT NULL,
	[cutomerId] [varchar](50) NULL,
	[amount] [float] NULL,
	[paid] [float] NULL,
	[cheque] [float] NULL,
	[chequenumber] [varchar](50) NULL,
	[chequeDate] [date] NULL,
	[datetime] [datetime] NULL,
	[checkCodeNo] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[chequeInvoiceRetailBak]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[chequeInvoiceRetailBak](
	[invoiceID] [int] NOT NULL,
	[cutomerId] [varchar](50) NULL,
	[amount] [float] NULL,
	[paid] [float] NULL,
	[cheque] [float] NULL,
	[chequenumber] [varchar](50) NOT NULL,
	[chequeDate] [date] NULL,
	[datetime] [datetime] NULL,
	[checkCodeNo] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[chequeInvoiceRetail2]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[chequeInvoiceRetail2](
	[invoiceID] [int] NOT NULL,
	[cutomerId] [varchar](50) NULL,
	[amount] [float] NULL,
	[paid] [float] NULL,
	[cheque] [float] NULL,
	[chequenumber] [varchar](50) NOT NULL,
	[chequeDate] [date] NULL,
	[datetime] [datetime] NULL,
	[checkCodeNo] [varchar](50) NULL,
	[deposit] [bit] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[chequeInvoiceRetail]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[chequeInvoiceRetail](
	[invoiceID] [int] NOT NULL,
	[cutomerId] [varchar](50) NULL,
	[amount] [float] NULL,
	[paid] [float] NULL,
	[cheque] [float] NULL,
	[chequenumber] [varchar](50) NOT NULL,
	[chequeDate] [date] NULL,
	[datetime] [datetime] NULL,
	[checkCodeNo] [varchar](50) NULL,
	[deposit] [bit] NULL,
	[bankName] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[chequeGRNHistorey]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[chequeGRNHistorey](
	[grnId] [int] NOT NULL,
	[supplierId] [varchar](50) NULL,
	[amount] [float] NULL,
	[paid] [float] NULL,
	[cheque] [float] NULL,
	[chequenumber] [varchar](50) NULL,
	[chequeDate] [date] NULL,
	[datetime] [datetime] NULL,
	[checkCodeNo] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[chequeGRN2]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[chequeGRN2](
	[invoiceID] [int] NOT NULL,
	[cutomerId] [varchar](50) NULL,
	[amount] [float] NULL,
	[paid] [float] NULL,
	[cheque] [float] NULL,
	[chequenumber] [varchar](50) NOT NULL,
	[chequeDate] [date] NULL,
	[datetime] [datetime] NULL,
	[checkCodeNo] [varchar](50) NULL,
	[deposit] [bit] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[chequeGRN]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[chequeGRN](
	[invoiceID] [int] NOT NULL,
	[cutomerId] [varchar](50) NULL,
	[amount] [float] NULL,
	[paid] [float] NULL,
	[cheque] [float] NULL,
	[chequenumber] [varchar](50) NOT NULL,
	[chequeDate] [date] NULL,
	[datetime] [datetime] NULL,
	[checkCodeNo] [varchar](50) NULL,
	[deposit] [bit] NULL,
	[bankName] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[chequeDetail]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[chequeDetail](
	[id] [int] NOT NULL,
	[amount] [float] NULL,
	[chequenumber] [varchar](50) NOT NULL,
	[chequeDate] [date] NULL,
	[datetime] [datetime] NULL,
	[checkCodeNo] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[category3]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[category3](
	[name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_category3] PRIMARY KEY CLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[category2]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[category2](
	[name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_category2] PRIMARY KEY CLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[category1]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[category1](
	[name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_category1] PRIMARY KEY CLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cashTemp]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cashTemp](
	[id] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cashSummery_2]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cashSummery_2](
	[reason] [varchar](50) NULL,
	[remark] [varchar](max) NULL,
	[amount] [float] NULL,
	[date] [date] NULL
) ON [PRIMARY]
SET ANSI_PADDING OFF
ALTER TABLE [dbo].[cashSummery_2] ADD [userID] [varchar](50) NULL
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cashSummery]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cashSummery](
	[reason] [varchar](max) NULL,
	[remark] [varchar](max) NULL,
	[amount] [float] NULL,
	[date] [date] NULL
) ON [PRIMARY]
SET ANSI_PADDING OFF
ALTER TABLE [dbo].[cashSummery] ADD [userID] [varchar](max) NULL
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cashOpeningBalance]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cashOpeningBalance](
	[id] [varchar](50) NULL,
	[date] [date] NULL,
	[amount] [float] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cashInvoiceS]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cashInvoiceS](
	[invoiceID] [int] NOT NULL,
	[cutomerID] [varchar](50) NULL,
	[amount] [float] NULL,
	[datetime] [datetime] NULL,
 CONSTRAINT [PK_cashInvoiceS] PRIMARY KEY CLUSTERED 
(
	[invoiceID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cashInvoiceRetailHistorey]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[cashInvoiceRetailHistorey](
	[invoiceID] [varchar](50) NOT NULL,
	[cutomerID] [varchar](50) NULL,
	[amount] [float] NULL,
	[datetime] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cashInvoiceRetail]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cashInvoiceRetail](
	[invoiceID] [int] NOT NULL,
	[cutomerID] [varchar](50) NULL,
	[amount] [float] NULL,
	[datetime] [datetime] NULL,
 CONSTRAINT [PK_cashInvoiceRetail] PRIMARY KEY CLUSTERED 
(
	[invoiceID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cashInvoiceHistorey]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cashInvoiceHistorey](
	[invoiceID] [varchar](50) NOT NULL,
	[cutomerID] [varchar](50) NULL,
	[amount] [float] NULL,
	[datetime] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cashInvoice]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cashInvoice](
	[invoiceID] [int] NOT NULL,
	[cutomerID] [varchar](50) NULL,
	[amount] [float] NULL,
	[datetime] [datetime] NULL,
 CONSTRAINT [PK_cashInvoice] PRIMARY KEY CLUSTERED 
(
	[invoiceID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cashGRNHistorey]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[cashGRNHistorey](
	[grnID] [int] NOT NULL,
	[supplierID] [varchar](50) NULL,
	[amount] [float] NULL,
	[datetime] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cashGRN]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cashGRN](
	[invoiceID] [int] NOT NULL,
	[cutomerID] [varchar](50) NULL,
	[amount] [float] NULL,
	[datetime] [datetime] NULL,
 CONSTRAINT [PK_cashInvoiceggRetail] PRIMARY KEY CLUSTERED 
(
	[invoiceID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cashBF]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cashBF](
	[date] [date] NULL,
	[amount] [float] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[cashBalance]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cashBalance](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userID] [varchar](50) NULL,
	[memo] [varchar](max) NULL,
	[credit] [bit] NULL,
	[amount] [float] NULL,
	[date] [date] NULL,
 CONSTRAINT [PK_cashBalance] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cardInvoiceRetailBak]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cardInvoiceRetailBak](
	[cutomerID] [varchar](50) NULL,
	[amount] [float] NULL,
	[datetime] [datetime] NULL,
	[paid] [float] NULL,
	[balance] [float] NULL,
	[cardNo] [varchar](50) NULL,
	[ccv] [varchar](50) NULL,
	[nameOnCard] [varchar](100) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cardInvoiceRetail]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cardInvoiceRetail](
	[invoiceID] [int] NOT NULL,
	[cutomerID] [varchar](50) NULL,
	[amount] [float] NULL,
	[datetime] [datetime] NULL,
	[paid] [float] NULL,
	[balance] [float] NULL,
	[cardNo] [varchar](50) NULL,
	[ccv] [varchar](50) NULL,
	[nameOnCard] [varchar](100) NULL,
 CONSTRAINT [PK_cardInvoiceRetail] PRIMARY KEY CLUSTERED 
(
	[invoiceID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cardGRNBak]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cardGRNBak](
	[invoiceID] [int] NOT NULL,
	[cutomerID] [varchar](50) NULL,
	[amount] [float] NULL,
	[datetime] [datetime] NULL,
	[paid] [float] NULL,
	[balance] [float] NULL,
	[cardNo] [varchar](50) NULL,
	[ccv] [varchar](50) NULL,
	[nameOnCard] [varchar](100) NULL,
 CONSTRAINT [PK_cardInvoiceRetailGNBak] PRIMARY KEY CLUSTERED 
(
	[invoiceID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cardGRN]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cardGRN](
	[invoiceID] [int] NOT NULL,
	[cutomerID] [varchar](50) NULL,
	[amount] [float] NULL,
	[datetime] [datetime] NULL,
	[paid] [float] NULL,
	[balance] [float] NULL,
	[cardNo] [varchar](50) NULL,
	[ccv] [varchar](50) NULL,
	[nameOnCard] [varchar](100) NULL,
 CONSTRAINT [PK_caFFrdInvoiceRetail] PRIMARY KEY CLUSTERED 
(
	[invoiceID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[canselInvoice]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[canselInvoice](
	[id] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[brand]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[brand](
	[name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_brand] PRIMARY KEY CLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[bankAccountStatment]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[bankAccountStatment](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[accountid] [int] NULL,
	[number] [varchar](50) NULL,
	[type] [varchar](50) NULL,
	[payee] [varchar](100) NULL,
	[memo] [varchar](max) NULL,
	[isDeposit] [bit] NULL,
	[credit] [bit] NULL,
	[date] [date] NULL,
	[amount] [float] NULL,
	[accountFor] [varchar](50) NULL,
	[statementID] [varchar](50) NULL,
 CONSTRAINT [PK_bankAccountStatmentCard] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[bankAccounts]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[bankAccounts](
	[id] [int] NOT NULL,
	[name] [varchar](50) NULL,
	[bankName] [varchar](50) NULL,
	[acountNo] [varchar](50) NULL,
	[openingBalance] [float] NULL,
	[openingDate] [date] NULL,
 CONSTRAINT [PK_bankAccounts] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[bank]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[bank](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BANKNAME] [varchar](50) NULL,
	[bankCode] [varchar](50) NULL,
 CONSTRAINT [PK_bank] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[auditItem2]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[auditItem2](
	[code] [varchar](50) NOT NULL,
	[brand] [varchar](50) NULL,
	[categorey] [varchar](50) NULL,
	[description] [varchar](max) NULL,
	[remark] [varchar](50) NULL,
	[rate] [varchar](50) NULL,
	[purchasingPrice] [float] NULL,
	[retailPrice] [float] NULL,
	[billingPrice] [float] NULL,
	[qty] [float] NULL,
	[warrentyCode] [varchar](50) NULL,
	[user] [varchar](50) NULL,
	[type] [varchar](50) NULL,
	[date] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[attendance]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[attendance](
	[empid] [varchar](50) NULL,
	[date] [date] NULL,
	[present] [bit] NULL,
	[punish] [bit] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[advancedEmp]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[advancedEmp](
	[empid] [varchar](50) NULL,
	[amount] [float] NULL,
	[date] [date] NULL,
	[month] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[accounts]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[accounts](
	[id] [int] NULL,
	[name] [varchar](150) NULL,
	[type] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[accountChequePayment]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[accountChequePayment](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[accountID] [int] NULL,
	[isDefa] [bit] NULL,
 CONSTRAINT [PK_accountDefa] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[accountCardPayment]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[accountCardPayment](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[accountID] [int] NULL,
	[isDefa] [bit] NULL,
 CONSTRAINT [PK_accountDefaCard] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[a_tempCustomer]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[a_tempCustomer](
	[name] [varchar](50) NULL,
	[contact] [varchar](50) NULL,
	[amount] [float] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[a]    Script Date: 03/03/2020 17:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[a](
	[sa] [nchar](10) NOT NULL,
 CONSTRAINT [PK_a] PRIMARY KEY CLUSTERED 
(
	[sa] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Default [DF__supplierB__faxNu__14E61A24]    Script Date: 03/03/2020 17:23:55 ******/
ALTER TABLE [dbo].[supplierBak] ADD  DEFAULT ('') FOR [faxNumber]
GO
/****** Object:  Default [DF__paysheet__epf12__12FDD1B2]    Script Date: 03/03/2020 17:23:55 ******/
ALTER TABLE [dbo].[paysheet] ADD  DEFAULT ('0') FOR [epf12]
GO
/****** Object:  Default [DF__paysheet__etf3__13F1F5EB]    Script Date: 03/03/2020 17:23:55 ******/
ALTER TABLE [dbo].[paysheet] ADD  DEFAULT ('0') FOR [etf3]
GO
/****** Object:  Default [DF__itemState__purch__1209AD79]    Script Date: 03/03/2020 17:23:55 ******/
ALTER TABLE [dbo].[itemStatement] ADD  DEFAULT ('0') FOR [purchsingPrice]
GO
/****** Object:  Default [DF__item__id__0F2D40CE]    Script Date: 03/03/2020 17:23:55 ******/
ALTER TABLE [dbo].[item] ADD  DEFAULT ('0') FOR [id]
GO
/****** Object:  Default [DF__item__isItem__10216507]    Script Date: 03/03/2020 17:23:55 ******/
ALTER TABLE [dbo].[item] ADD  DEFAULT ('false') FOR [isItem]
GO
/****** Object:  Default [DF__item__sparePart__11158940]    Script Date: 03/03/2020 17:23:55 ******/
ALTER TABLE [dbo].[item] ADD  DEFAULT ('0') FOR [sparePart]
GO
/****** Object:  Default [DF__grnDetail__qtyOu__0D44F85C]    Script Date: 03/03/2020 17:23:55 ******/
ALTER TABLE [dbo].[grnDetail] ADD  DEFAULT ('0') FOR [qtyOut]
GO
/****** Object:  Default [DF__grnDetail__isQty__0E391C95]    Script Date: 03/03/2020 17:23:55 ******/
ALTER TABLE [dbo].[grnDetail] ADD  DEFAULT ('false') FOR [isQtyIn]
GO
/****** Object:  Default [DF__GRN__invoiceNo__0B5CAFEA]    Script Date: 03/03/2020 17:23:55 ******/
ALTER TABLE [dbo].[GRN] ADD  DEFAULT ('') FOR [invoiceNo]
GO
/****** Object:  Default [DF__GRN__grnDate__0C50D423]    Script Date: 03/03/2020 17:23:55 ******/
ALTER TABLE [dbo].[GRN] ADD  DEFAULT ('') FOR [grnDate]
GO
/****** Object:  Default [DF__fullServi__messa__0A688BB1]    Script Date: 03/03/2020 17:23:55 ******/
ALTER TABLE [dbo].[fullService] ADD  DEFAULT ('') FOR [messageSend]
GO
/****** Object:  Default [DF__Emp__incentive__7D0E9093]    Script Date: 03/03/2020 17:23:55 ******/
ALTER TABLE [dbo].[Emp] ADD  DEFAULT ('0') FOR [incentive]
GO
/****** Object:  Default [DF__Emp__allowances__7E02B4CC]    Script Date: 03/03/2020 17:23:55 ******/
ALTER TABLE [dbo].[Emp] ADD  DEFAULT ('0') FOR [allowances]
GO
/****** Object:  Default [DF__Emp__meal__7EF6D905]    Script Date: 03/03/2020 17:23:55 ******/
ALTER TABLE [dbo].[Emp] ADD  DEFAULT ('0') FOR [meal]
GO
/****** Object:  Default [DF__Emp__gross__7FEAFD3E]    Script Date: 03/03/2020 17:23:55 ******/
ALTER TABLE [dbo].[Emp] ADD  DEFAULT ('0') FOR [gross]
GO
/****** Object:  Default [DF__Emp__resgin__00DF2177]    Script Date: 03/03/2020 17:23:55 ******/
ALTER TABLE [dbo].[Emp] ADD  DEFAULT ('false') FOR [resgin]
GO
/****** Object:  Default [DF__Emp__bouns__01D345B0]    Script Date: 03/03/2020 17:23:55 ******/
ALTER TABLE [dbo].[Emp] ADD  DEFAULT ('0') FOR [bouns]
GO
/****** Object:  Default [DF__Emp__punish__02C769E9]    Script Date: 03/03/2020 17:23:55 ******/
ALTER TABLE [dbo].[Emp] ADD  DEFAULT ('0') FOR [punish]
GO
/****** Object:  Default [DF__Emp__holiday__03BB8E22]    Script Date: 03/03/2020 17:23:55 ******/
ALTER TABLE [dbo].[Emp] ADD  DEFAULT ('') FOR [holiday]
GO
/****** Object:  Default [DF__Emp__advanced__04AFB25B]    Script Date: 03/03/2020 17:23:55 ******/
ALTER TABLE [dbo].[Emp] ADD  DEFAULT ('0') FOR [advanced]
GO
/****** Object:  Default [DF__Emp__loan__05A3D694]    Script Date: 03/03/2020 17:23:55 ******/
ALTER TABLE [dbo].[Emp] ADD  DEFAULT ('0') FOR [loan]
GO
/****** Object:  Default [DF__Emp__nic__0697FACD]    Script Date: 03/03/2020 17:23:55 ******/
ALTER TABLE [dbo].[Emp] ADD  DEFAULT ('0') FOR [nic]
GO
/****** Object:  Default [DF__Emp__isEpf__078C1F06]    Script Date: 03/03/2020 17:23:55 ******/
ALTER TABLE [dbo].[Emp] ADD  DEFAULT ('false') FOR [isEpf]
GO
/****** Object:  Default [DF__Emp__isExecutive__0880433F]    Script Date: 03/03/2020 17:23:55 ******/
ALTER TABLE [dbo].[Emp] ADD  DEFAULT ('false') FOR [isExecutive]
GO
/****** Object:  Default [DF__Emp__epfNO__09746778]    Script Date: 03/03/2020 17:23:55 ******/
ALTER TABLE [dbo].[Emp] ADD  DEFAULT ('') FOR [epfNO]
GO
/****** Object:  Default [DF__customerB__faxNu__7C1A6C5A]    Script Date: 03/03/2020 17:23:55 ******/
ALTER TABLE [dbo].[customerBak] ADD  DEFAULT ('') FOR [faxNumber]
GO
/****** Object:  Default [DF__customer__isEpf__22401542]    Script Date: 03/03/2020 17:23:55 ******/
ALTER TABLE [dbo].[customer] ADD  DEFAULT ('false') FOR [isEpf]
GO
/****** Object:  Default [DF__customer__isExec__2334397B]    Script Date: 03/03/2020 17:23:55 ******/
ALTER TABLE [dbo].[customer] ADD  DEFAULT ('false') FOR [isExecutive]
GO
/****** Object:  Default [DF__customer__epfNO__24285DB4]    Script Date: 03/03/2020 17:23:55 ******/
ALTER TABLE [dbo].[customer] ADD  DEFAULT ('') FOR [epfNO]
GO
/****** Object:  Default [DF__attendanc__punis__7849DB76]    Script Date: 03/03/2020 17:23:55 ******/
ALTER TABLE [dbo].[attendance] ADD  DEFAULT ('false') FOR [punish]
GO
