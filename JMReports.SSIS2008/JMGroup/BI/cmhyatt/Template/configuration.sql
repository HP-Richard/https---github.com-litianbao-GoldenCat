USE [JMGroup]
GO

INSERT INTO [dbo].[Configuration]
           ([type]
           ,[job]
           ,[package]
           ,[task]
           ,[name]
           ,[value]
           ,[CreatedBy]
           ,[UpdatedDate]
           ,[UpdatedBy])

SELECT 'file','JMGroup_CreateDailyAccountingFinanceCSVforFTP','JMGroup_CreateDailyAccountingFinanceCSVforFTP',NULL,'fileBase','G:\JMGroup\BI\','2014-08-06 14:42:57.740','Richard','2014-08-04 09:50:33.473',NULL 
UNION ALL SELECT 'ftp','JMGroup_CreateDailyAccountingFinanceCSVforFTP','JMGroup_CreateDailyAccountingFinanceCSVforFTP',NULL,'cmd','C:\Windows\System32\cmd.exe','2014-08-06 14:42:57.740','Richard','2014-08-04 09:50:33.473',NULL 
UNION ALL SELECT 'ftp','JMGroup_CreateDailyAccountingFinanceCSVforFTP','JMGroup_CreateDailyAccountingFinanceCSVforFTP',NULL,'cmdZipArguements','/c makecab  /D CompressionType=LZX /L G:\JMGroup\BI\out\ G:\JMGroup\BI\in\gl06.csv','2014-08-06 14:42:57.740','Richard','2014-08-04 09:50:33.473',NULL 
UNION ALL SELECT 'ssis','JMGroup_CreateDailyAccountingFinanceCSVforFTP','JMGroup_CreateDailyAccountingFinanceCSVforFTP','Export Finance CSV','latestId','0','2014-08-06 14:42:57.740','BI_Staging_CreateYearlyRenewCSVforFTP','2014-08-04 09:50:33.473',NULL 
UNION ALL SELECT 'ssis','JMGroup_CreateDailyAccountingFinanceCSVforFTP','JMGroup_CreateDailyAccountingFinanceCSVforFTP',NULL,'companyCode','01','2014-08-06 14:42:57.740','Richard','2014-08-04 10:00:52.027',NULL 
UNION ALL SELECT 'ssis','JMGroup_CreateDailyAccountingFinanceCSVforFTP','JMGroup_CreateDailyAccountingFinanceCSVforFTP',NULL,'yearCode','14','2014-08-06 14:42:57.740','BI_Staging_CreateYearlyRenewCSVforFTP','2014-08-04 10:01:17.250',NULL 
UNION ALL SELECT 'ssis','JMGroup_CreateDailyAccountingFinanceCSVforFTP','JMGroup_CreateDailyAccountingFinanceCSVforFTP',NULL,'ftpFolder','/shhyatt/in','2014-08-06 14:42:57.740','Richard','2014-08-04 10:16:11.693',NULL 
UNION ALL SELECT 'ftp','JMGroup_CreateDailyAccountingFinanceCSVforFTP','JMGroup_CreateDailyAccountingFinanceCSVforFTP',NULL,'ftpUsername','shhyatt','2014-08-06 14:42:57.740','Richard','2014-08-04 10:16:22.210',NULL 
UNION ALL SELECT 'ftp','JMGroup_CreateDailyAccountingFinanceCSVforFTP','JMGroup_CreateDailyAccountingFinanceCSVforFTP',NULL,'pwd','shhyatt','2014-08-06 14:42:57.740','Richard','2014-08-04 10:16:26.607',NULL 
UNION ALL SELECT 'ssis','JMGroup_CreateDailyAccountingFinanceCSVforFTP','JMGroup_CreateDailyAccountingFinanceCSVforFTP',NULL,'SQL_GetLatestRecords','SELECT [GL06001]       ,[GL06003]       ,[GL06004]       ,[GL06005]       ,[GL06012]       ,[GL06013]       ,[GL06014]       ,[GL06016]       ,[GL06018] FROM [Staging_Hotel].[dbo].[Staging_GL060114] WHERE [GL06016] BETWEEN 0 AND 687  ORDER BY [GL06016] ASC','2014-08-06 14:42:57.740','Richard','2014-08-04 11:03:24.123',NULL 
UNION ALL SELECT 'ssis','JMGroup_CreateDailyAccountingFinanceCSVforFTP','JMGroup_CreateDailyAccountingFinanceCSVforFTP',NULL,'maxId','0','2014-08-06 14:42:57.740','Richard','2014-08-04 11:41:09.990',NULL 
UNION ALL SELECT 'ssis','JMGroup_CreateDailyAccountingFinanceCSVforFTP','JMGroup_CreateDailyAccountingFinanceCSVforFTP',NULL,'maxRetrievingCount','100000','2014-08-06 14:42:57.740','Richard','2014-08-04 11:41:29.920',NULL 
UNION ALL SELECT 'ssis','JMGroup_CreateDailyAccountingFinanceCSVforFTP','JMGroup_CreateDailyAccountingFinanceCSVforFTP',NULL,'accountingTable','GL060114','2014-08-06 14:42:57.740','Richard','2014-08-04 13:35:07.003',NULL 
UNION ALL SELECT 'ssis','JMGroup_CreateDailyAccountingFinanceCSVforFTP','JMGroup_CreateDailyAccountingFinanceCSVforFTP',NULL,'workingDirectory','G:\JMGroup\BI\in\','2014-08-06 14:42:57.740','Richard','2014-08-04 13:44:41.003',NULL 
UNION ALL SELECT 'ssis','JMGroup_CreateDailyAccountingFinanceCSVforFTP','JMGroup_CreateDailyAccountingFinanceCSVforFTP',NULL,'WinSCP','C:\Program Files (x86)\WinSCP\WinSCP.exe','2014-08-06 14:42:57.740','Richard','2014-08-04 14:05:31.203',NULL 
UNION ALL SELECT 'ssis','JMGroup_CreateDailyAccountingFinanceCSVforFTP','JMGroup_CreateDailyAccountingFinanceCSVforFTP',NULL,'ftpArguments','/console /script=G:\JMGroup\BI\Template\FTPScript.txt','2014-08-06 14:42:57.740','Richard','2014-08-04 14:06:31.637',NULL 
UNION ALL SELECT 'ftp','JMGroup_CreateDailyAccountingFinanceCSVforFTP','JMGroup_CreateDailyAccountingFinanceCSVforFTP',NULL,'ftpWorkingDirectory','C:\Program Files (x86)\WinSCP','2014-08-06 14:42:57.740','Richard','2014-08-04 14:06:45.030',NULL 
UNION ALL SELECT 'ftp','JMGroup_CreateDailyAccountingFinanceCSVforFTP','JMGroup_CreateDailyAccountingFinanceCSVforFTP',NULL,'ftpServer','180.168.37.135','2014-08-06 14:42:57.740','Richard','2014-08-04 16:14:59.957',NULL 
UNION ALL SELECT 'ftp','JMGroup_CreateDailyAccountingFinanceCSVforFTP','JMGroup_CreateDailyAccountingFinanceCSVforFTP',NULL,'ftpPwd','shhyatt','2014-08-06 14:42:57.740','Richard','2014-08-04 16:15:44.850',NULL 
UNION ALL SELECT 'ftp','JMGroup_CreateDailyAccountingFinanceCSVforFTP','JMGroup_CreateDailyAccountingFinanceCSVforFTP',NULL,'ftpAddr','ftp://shhyatt:shhyatt@180.168.37.135','2014-08-06 14:42:57.740',NULL,'2014-08-04 17:05:27.247',NULL 
UNION ALL SELECT 'ftp','JMGroup_CreateDailyAccountingFinanceCSVforFTP','JMGroup_CreateDailyAccountingFinanceCSVforFTP',NULL,'ftpScript','# Automatically abort script on errors
option batch abort
# Disable overwrite confirmations that conflict with the previous
option confirm off
# Connect using a password
# open sftp://user:password@example.com/ -hostkey="ssh-rsa 2048 xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx"
# Connect
# Connect as a different user
open [ftpAddr]
# Change the remote directory
cd [ftpFolder]
# Upload the file to current working directory
put [[uploadFile]
# Disconnect
close
# Exit WinSCP
exit
','2014-08-06 14:42:57.740',NULL,'2014-08-04 17:14:17.607',NULL
GO


