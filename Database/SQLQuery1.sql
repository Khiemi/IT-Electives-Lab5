USE SchoolDb;

CREATE TABLE Students (
    StudentId NVARCHAR(20) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Course NVARCHAR(100) NOT NULL
);