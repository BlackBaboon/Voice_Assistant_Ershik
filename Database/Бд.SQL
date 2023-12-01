use master;
DROP DATABASE IF EXISTS VoiceAssistant_Ershik;
CREATE DATABASE VoiceAssistant_Ershik;
use VoiceAssistant_Ershik;

CREATE TABLE [Phrase]
(
 [Profile_name] Nvarchar(16) NOT NULL,
 [Phrase] Nvarchar(24) NOT NULL,
 [Phrase_desc] Nvarchar(200) NOT NULL
)
go

-- Add keys for table Phrase

ALTER TABLE [Phrase] ADD CONSTRAINT [Unique_Identifier1] PRIMARY KEY ([Phrase],[Profile_name])
go

-- Table Profile

CREATE TABLE [Profile]
(
 [Profile_name] Nvarchar(16) NOT NULL,
 [Login] Nvarchar(24) NOT NULL,
 [Password] Nvarchar(24) NOT NULL
)
go

-- Add keys for table Profile

ALTER TABLE [Profile] ADD CONSTRAINT [Unique_Identifier2] PRIMARY KEY ([Profile_name])
go

-- Table Script

CREATE TABLE [Script]
(
 [Profile_name] Nvarchar(16) NOT NULL,
 [Script_caption] Nvarchar(24) NOT NULL,
 [Script_desc] Nvarchar(200) NOT NULL
)
go

-- Add keys for table Script

ALTER TABLE [Script] ADD CONSTRAINT [Unique_Identifier3] PRIMARY KEY ([Profile_name],[Script_caption])
go

-- Table Command_sequence

CREATE TABLE [Command_sequence]
(
 [Profile_name] Nvarchar(16) NOT NULL,
 [Script_caption] Nvarchar(24) NOT NULL,
 [Command_sequence_number] Int NOT NULL,
 [Command] Nvarchar(500) NOT NULL
)
go

-- Add keys for table Command_sequence

ALTER TABLE [Command_sequence] ADD CONSTRAINT [PK_Command_sequence] PRIMARY KEY ([Command_sequence_number],[Profile_name],[Script_caption])
go

-- Table Script_bind

CREATE TABLE [Script_bind]
(
 [Profile_name] Nvarchar(16) NOT NULL,
 [Phrase] Nvarchar(24) NOT NULL,
 [Script_caption] Nvarchar(24) NOT NULL,
 [Script_sequence_number] Int NOT NULL
)
go
ALTER TABLE [Script_bind] ADD CONSTRAINT [PK_Script_bind] PRIMARY KEY ([Script_sequence_number],[Phrase],[Profile_name],[Script_caption])
go
ALTER TABLE [Phrase] ADD CONSTRAINT [Привязывает] FOREIGN KEY ([Profile_name]) REFERENCES [Profile] ([Profile_name]) ON UPDATE CASCADE ON DELETE CASCADE
go
ALTER TABLE [Command_sequence] ADD CONSTRAINT [Выполняет] FOREIGN KEY ([Profile_name], [Script_caption]) REFERENCES [Script] ([Profile_name], [Script_caption]) ON UPDATE CASCADE ON DELETE CASCADE
go
ALTER TABLE [Script_bind] ADD CONSTRAINT [Активируется] FOREIGN KEY ([Profile_name], [Script_caption]) REFERENCES [Script] ([Profile_name], [Script_caption]) ON UPDATE CASCADE ON DELETE CASCADE
go
ALTER TABLE [Script_bind] ADD CONSTRAINT [Активирует] FOREIGN KEY ([Phrase], [Profile_name]) REFERENCES [Phrase] ([Phrase], [Profile_name]) ON UPDATE CASCADE ON DELETE CASCADE
go
ALTER TABLE [Script] ADD CONSTRAINT [Привязывает_] FOREIGN KEY ([Profile_name]) REFERENCES [Profile] ([Profile_name]) ON UPDATE NO ACTION ON DELETE NO ACTION
go




