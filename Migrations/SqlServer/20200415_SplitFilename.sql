alter table dbo.Scans Add PartialFilename nvarchar(max) not null default '';
GO

declare @RootDocumentPath nvarchar(max)

set @RootDocumentPath = '\\ubuntu-nas\secure\Digitaal Archief'

update dbo.Scans
set PartialFilename = SUBSTRING(filename, len(@RootDocumentPath)+2, 2000)

Alter table dbo.scans drop column Filename

alter table dbo.scans add FileName as ('\\ubuntu-nas\secure\Digitaal Archief\'+PartialFileName)