CREATE DATABASE [ABCData];
GO

USE [ABCData];
GO

create table Cineplex
(
	CineplexID int not null identity primary key,
	Location nvarchar(max) not null,
	ShortDescription nvarchar(max) not null,
	LongDescription nvarchar(max) not null,
	ImageUrl nvarchar(max) null
);
GO

create table Enquiry
(
	EnquiryID int not null identity primary key,
	Email nvarchar(max) not null,
	Message nvarchar(max) not null
);
GO

create table MovieComingSoon
(
	MovieComingSoonID int not null identity primary key,
	Title nvarchar(max) not null,
	ShortDescription nvarchar(max) not null,
	LongDescription nvarchar(max) not null,
	ImageUrl nvarchar(max) null
);
GO

create table Movie
(
	MovieID int not null identity primary key,
	Title nvarchar(max) not null,
	ShortDescription nvarchar(max) not null,
	LongDescription nvarchar(max) not null,
	ImageUrl nvarchar(max) null,
	Price money not null
);
GO

create table CineplexMovie
(
	CineplexID int not null foreign key references Cineplex (CineplexID),
	MovieID int not null foreign key references Movie (MovieID),
	primary key (CineplexID, MovieID),
);
GO

create table SessionTime
(
	SessionID int not null identity primary key,
	CineplexID int not null foreign key references Cineplex (CineplexID),
	MovieID int not null foreign key references Movie (MovieID),
	MovieTime DateTime not null,
);
GO

create table Reservation
(
	ReservationID int not null identity primary key,
	EnquiryID int not null foreign key references Enquiry (EnquiryID),
	SessionID int not null foreign key references SessionTime (SessionID),
	SeatID int not null
);
GO

create table EventsInfo
(
	EventsID int not null identity primary key,
	Information nvarchar(max) null
);
GO

create table EnquiryEvents
(
	EventsID int not null foreign key references EventsInfo (EventsID),
	EnquiryID int not null foreign key references Enquiry (EnquiryID),
	primary key (EventsID, EnquiryID),
);
