SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DocumentTypeCustomFields](
	[Id] [varchar](36) NOT NULL,
	[DocumentTypeId] [varchar](36) NOT NULL,
	[FieldName] [nvarchar](255) NOT NULL,
	[FieldType] [nvarchar](20) NOT NULL,
	[Mandatory] [bit] NOT NULL,
 CONSTRAINT [PK_DocumentTypeCustomFields] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)
)
GO

ALTER TABLE [dbo].[DocumentTypeCustomFields]  WITH CHECK ADD  CONSTRAINT [FK_DocumentTypeCustomFields_DocumentTypes] FOREIGN KEY([DocumentTypeId])
REFERENCES [dbo].[DocumentTypes] ([Id])
GO

ALTER TABLE [dbo].[DocumentTypeCustomFields] CHECK CONSTRAINT [FK_DocumentTypeCustomFields_DocumentTypes]
GO


------------------------------------------------------------------------------------------------------


CREATE TABLE [dbo].[DocumentCustomFields](
	[Id] [varchar](36) NOT NULL,
	[DocumentId] [varchar](36) NOT NULL,
	[DocumentTypeCustomFieldId] [varchar](36) NOT NULL,
	[StringValue] [nvarchar](max) NULL,
	[IntValue] [bigint] NULL,
	[DecimalValue] [numeric](18, 6) NULL,
	[BooleanValue] [bit] NULL,
	[DateValue] [datetime2](7) NULL,
 CONSTRAINT [PK_DocumentCustomFields] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)
)
GO

ALTER TABLE [dbo].[DocumentCustomFields]  WITH CHECK ADD  CONSTRAINT [FK_DocumentCustomFields_Documents] FOREIGN KEY([DocumentId])
REFERENCES [dbo].[Documents] ([Id])
GO

ALTER TABLE [dbo].[DocumentCustomFields] CHECK CONSTRAINT [FK_DocumentCustomFields_Documents]
GO

ALTER TABLE [dbo].[DocumentCustomFields]  WITH CHECK ADD  CONSTRAINT [FK_DocumentCustomFields_DocumentTypeCustomFields] FOREIGN KEY([DocumentTypeCustomFieldId])
REFERENCES [dbo].[DocumentTypeCustomFields] ([Id])
GO

ALTER TABLE [dbo].[DocumentCustomFields] CHECK CONSTRAINT [FK_DocumentCustomFields_DocumentTypeCustomFields]
GO
