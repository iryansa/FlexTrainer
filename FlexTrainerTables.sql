CREATE DATABASE PROJECT;
use PROJECT;

-- Creating the necessary tables for the application
create table users(
	username varchar(20) primary key,
	Name varchar(50) not null,
	email varchar(50) not null,
	password varchar(20) not null,
	typeuser varchar(10) not null,
	expertise varchar(50),
	qualification varchar(50)
	);


Create table workoutPlan(
	workout_id INT NOT NULL IDENTITY PRIMARY KEY,
	workout_name varchar(50) not null,
	goal varchar(50) not null,
	exp_level varchar(50) not null,
	author varchar(20) not null,
	authortype varchar(10) not null,
	schedule varchar(10) not null
	
	);

create table excercise(
	workoutday int not null,
	workoutId int not null,
	exercisename varchar(50) not null,
	machine varchar(50),
	sets int,
	reps int,
	musclegroup varchar(50),

	foreign key (workoutId) references workoutPlan(workout_id),
	primary key(workoutday, workoutId, exercisename)
	);

create table dietPlan(
	diet_id INT NOT NULL IDENTITY PRIMARY KEY,
	diet_name varchar(50) not null,
	goal varchar(50) not null,
	typeDiet varchar(50) not null,
	calories int not null,
	author varchar(20) not null,
	authortype varchar(10) not null
	);

create table meal(
	dietday int not null,
	diet_Id int not null,
	mealdescription varchar(50),
	kcal int,
	allergens varchar(50),
	foreign key (diet_Id) references dietPlan(diet_id),
	primary key(dietday, diet_Id)
	);


CREATE TABLE appointmentTable (
	apt_id INT IDENTITY PRIMARY KEY,
	trainer_username varchar(20) NOT NULL,
	member_username varchar(20),
	time_slot datetime NOT NULL,
	Status varchar(20) NOT NULL 

	FOREIGN KEY (trainer_username) REFERENCES users(username)
);


create table trainerFeedback (
	apt_id INT PRIMARY KEY,
	rating INT NOT NULL,
	description varchar(50)

	FOREIGN KEY (apt_id) REFERENCES appointmentTable(apt_id)
);



create table memberworkout(
	memberusername varchar(20),
	workoutId int

	foreign key (memberusername) references users(username),
	foreign key (workoutId) references workoutPlan(workout_id),
	primary key (memberusername , workoutId)
	);

create table memberdiet(
	memberusername varchar(20),
	dietId int

	foreign key (memberusername) references users(username),
	foreign key (dietId) references dietPlan(diet_id),
	primary key (memberusername , dietId)
	);


create table gyms(
	gymowner varchar(20),
	gymname varchar(50),
	businessplan varchar(50),
	facility varchar(50),
	currentmembers int,
	approval varchar(20),

	foreign key (gymowner) references users(username),
	primary key(gymowner, gymname)
	)




create table membergym(
	username varchar(20),
	gymname varchar(50),

	foreign key(username) references users(username),
	primary key(username, gymname)
	);
	
create table trainergym(
	username varchar(20),
	gymname varchar(50),
	approved varchar(50),

	foreign key(username) references users(username),
	primary key(username, gymname)
	);




CREATE TABLE AuditTrail (
    AuditTrailID INT IDENTITY PRIMARY KEY,
    TableName VARCHAR(50) NOT NULL,
    Operation VARCHAR(10) NOT NULL,
    AuditDateTime DATETIME DEFAULT CURRENT_TIMESTAMP
);

drop table AuditTrail

CREATE TRIGGER Users_AuditTrigger
ON users
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    DECLARE @Operation VARCHAR(10);
    DECLARE @TableName VARCHAR(50) = 'users';
    
    IF EXISTS (SELECT * FROM inserted)
    BEGIN
        IF EXISTS (SELECT * FROM deleted)
            SET @Operation = 'UPDATE';
        ELSE
            SET @Operation = 'INSERT';
    END
    ELSE
        SET @Operation = 'DELETE';
    
    INSERT INTO AuditTrail (TableName, Operation)
    VALUES (@TableName, @Operation);
END;


CREATE TRIGGER WorkoutPlan_InsertAuditTrigger
ON workoutPlan
AFTER INSERT
AS
BEGIN
    DECLARE @Operation VARCHAR(10) = 'INSERT';
    DECLARE @TableName VARCHAR(50) = 'workoutPlan';
    
    INSERT INTO AuditTrail (TableName, Operation)
    VALUES (@TableName, @Operation);
END;

CREATE TRIGGER DietPlan_InsertAuditTrigger
ON dietPlan
AFTER INSERT
AS
BEGIN
    DECLARE @Operation VARCHAR(10) = 'INSERT';
    DECLARE @TableName VARCHAR(50) = 'dietPlan';
    
    INSERT INTO AuditTrail (TableName, Operation)
    VALUES (@TableName, @Operation);
END;


-- Trigger for appointmentTable Insertion
CREATE TRIGGER Insert_AppointmentTable_AuditTrigger
ON appointmentTable
AFTER INSERT
AS
BEGIN
    DECLARE @Operation VARCHAR(10) = 'INSERT';
    DECLARE @TableName VARCHAR(50) = 'appointmentTable';
    
    INSERT INTO AuditTrail (TableName, Operation)
    VALUES (@TableName, @Operation);
END;

-- Trigger for appointmentTable Update
CREATE TRIGGER Update_AppointmentTable_AuditTrigger
ON appointmentTable
AFTER UPDATE
AS
BEGIN
    DECLARE @Operation VARCHAR(10) = 'UPDATE';
    DECLARE @TableName VARCHAR(50) = 'appointmentTable';
    
    INSERT INTO AuditTrail (TableName, Operation)
    VALUES (@TableName, @Operation);
END;

-- Trigger for appointmentTable Deletion
CREATE TRIGGER Delete_AppointmentTable_AuditTrigger
ON appointmentTable
AFTER DELETE
AS
BEGIN
    DECLARE @Operation VARCHAR(10) = 'DELETE';
    DECLARE @TableName VARCHAR(50) = 'appointmentTable';
    
    INSERT INTO AuditTrail (TableName, Operation)
    VALUES (@TableName, @Operation);
END;

-- Trigger for trainerFeedback Insertion
CREATE TRIGGER Insert_TrainerFeedback_AuditTrigger
ON trainerFeedback
AFTER INSERT
AS
BEGIN
    DECLARE @Operation VARCHAR(10) = 'INSERT';
    DECLARE @TableName VARCHAR(50) = 'trainerFeedback';
    
    INSERT INTO AuditTrail (TableName, Operation)
    VALUES (@TableName, @Operation);
END;

-- Trigger for trainerFeedback Update
CREATE TRIGGER Update_TrainerFeedback_AuditTrigger
ON trainerFeedback
AFTER UPDATE
AS
BEGIN
    DECLARE @Operation VARCHAR(10) = 'UPDATE';
    DECLARE @TableName VARCHAR(50) = 'trainerFeedback';
    
    INSERT INTO AuditTrail (TableName, Operation)
    VALUES (@TableName, @Operation);
END;

-- Trigger for trainerFeedback Deletion
CREATE TRIGGER Delete_TrainerFeedback_AuditTrigger
ON trainerFeedback
AFTER DELETE
AS
BEGIN
    DECLARE @Operation VARCHAR(10) = 'DELETE';
    DECLARE @TableName VARCHAR(50) = 'trainerFeedback';
    
    INSERT INTO AuditTrail (TableName, Operation)
    VALUES (@TableName, @Operation);
END;

-- Trigger for memberworkout Insertion
CREATE TRIGGER Insert_MemberWorkout_AuditTrigger
ON memberworkout
AFTER INSERT
AS
BEGIN
    DECLARE @Operation VARCHAR(10) = 'INSERT';
    DECLARE @TableName VARCHAR(50) = 'memberworkout';
    
    INSERT INTO AuditTrail (TableName, Operation)
    VALUES (@TableName, @Operation);
END;

-- Trigger for memberworkout Update
CREATE TRIGGER Update_MemberWorkout_AuditTrigger
ON memberworkout
AFTER UPDATE
AS
BEGIN
    DECLARE @Operation VARCHAR(10) = 'UPDATE';
    DECLARE @TableName VARCHAR(50) = 'memberworkout';
    
    INSERT INTO AuditTrail (TableName, Operation)
    VALUES (@TableName, @Operation);
END;

-- Trigger for memberworkout Deletion
CREATE TRIGGER Delete_MemberWorkout_AuditTrigger
ON memberworkout
AFTER DELETE
AS
BEGIN
    DECLARE @Operation VARCHAR(10) = 'DELETE';
    DECLARE @TableName VARCHAR(50) = 'memberworkout';
    
    INSERT INTO AuditTrail (TableName, Operation)
    VALUES (@TableName, @Operation);
END;

-- Trigger for memberdiet Insertion
CREATE TRIGGER Insert_MemberDiet_AuditTrigger
ON memberdiet
AFTER INSERT
AS
BEGIN
    DECLARE @Operation VARCHAR(10) = 'INSERT';
    DECLARE @TableName VARCHAR(50) = 'memberdiet';
    
    INSERT INTO AuditTrail (TableName, Operation)
    VALUES (@TableName, @Operation);
END;

-- Trigger for memberdiet Update
CREATE TRIGGER Update_MemberDiet_AuditTrigger
ON memberdiet
AFTER UPDATE
AS
BEGIN
    DECLARE @Operation VARCHAR(10) = 'UPDATE';
    DECLARE @TableName VARCHAR(50) = 'memberdiet';
    
    INSERT INTO AuditTrail (TableName, Operation)
    VALUES (@TableName, @Operation);
END;

-- Trigger for memberdiet Deletion
CREATE TRIGGER Delete_MemberDiet_AuditTrigger
ON memberdiet
AFTER DELETE
AS
BEGIN
    DECLARE @Operation VARCHAR(10) = 'DELETE';
    DECLARE @TableName VARCHAR(50) = 'memberdiet';
    
    INSERT INTO AuditTrail (TableName, Operation)
    VALUES (@TableName, @Operation);
END;


-- Trigger for gyms Insertion
CREATE TRIGGER Insert_Gyms_AuditTrigger
ON gyms
AFTER INSERT
AS
BEGIN
    DECLARE @Operation VARCHAR(10) = 'INSERT';
    DECLARE @TableName VARCHAR(50) = 'gyms';
    
    INSERT INTO AuditTrail (TableName, Operation)
    VALUES (@TableName, @Operation);
END;

-- Trigger for gyms Update
CREATE TRIGGER Update_Gyms_AuditTrigger
ON gyms
AFTER UPDATE
AS
BEGIN
    DECLARE @Operation VARCHAR(10) = 'UPDATE';
    DECLARE @TableName VARCHAR(50) = 'gyms';
    
    INSERT INTO AuditTrail (TableName, Operation)
    VALUES (@TableName, @Operation);
END;

-- Trigger for gyms Deletion
CREATE TRIGGER Delete_Gyms_AuditTrigger
ON gyms
AFTER DELETE
AS
BEGIN
    DECLARE @Operation VARCHAR(10) = 'DELETE';
    DECLARE @TableName VARCHAR(50) = 'gyms';
    
    INSERT INTO AuditTrail (TableName, Operation)
    VALUES (@TableName, @Operation);
END;

-- Trigger for membergym Insertion
CREATE TRIGGER Insert_MemberGym_AuditTrigger
ON membergym
AFTER INSERT
AS
BEGIN
    DECLARE @Operation VARCHAR(10) = 'INSERT';
    DECLARE @TableName VARCHAR(50) = 'membergym';
    
    INSERT INTO AuditTrail (TableName, Operation)
    VALUES (@TableName, @Operation);
END;

-- Trigger for membergym Update
CREATE TRIGGER Update_MemberGym_AuditTrigger
ON membergym
AFTER UPDATE
AS
BEGIN
    DECLARE @Operation VARCHAR(10) = 'UPDATE';
    DECLARE @TableName VARCHAR(50) = 'membergym';
    
    INSERT INTO AuditTrail (TableName, Operation)
    VALUES (@TableName, @Operation);
END;

-- Trigger for membergym Deletion
CREATE TRIGGER Delete_MemberGym_AuditTrigger
ON membergym
AFTER DELETE
AS
BEGIN
    DECLARE @Operation VARCHAR(10) = 'DELETE';
    DECLARE @TableName VARCHAR(50) = 'membergym';
    
    INSERT INTO AuditTrail (TableName, Operation)
    VALUES (@TableName, @Operation);
END;

-- Trigger for trainergym Insertion
CREATE TRIGGER Insert_TrainerGym_AuditTrigger
ON trainergym
AFTER INSERT
AS
BEGIN
    DECLARE @Operation VARCHAR(10) = 'INSERT';
    DECLARE @TableName VARCHAR(50) = 'trainergym';
    
    INSERT INTO AuditTrail (TableName, Operation)
    VALUES (@TableName, @Operation);
END;

-- Trigger for trainergym Update
CREATE TRIGGER Update_TrainerGym_AuditTrigger
ON trainergym
AFTER UPDATE
AS
BEGIN
    DECLARE @Operation VARCHAR(10) = 'UPDATE';
    DECLARE @TableName VARCHAR(50) = 'trainergym';
    
    INSERT INTO AuditTrail (TableName, Operation)
    VALUES (@TableName, @Operation);
END;

-- Trigger for trainergym Deletion
CREATE TRIGGER Delete_TrainerGym_AuditTrigger
ON trainergym
AFTER DELETE
AS
BEGIN
    DECLARE @Operation VARCHAR(10) = 'DELETE';
    DECLARE @TableName VARCHAR(50) = 'trainergym';
    
    INSERT INTO AuditTrail (TableName, Operation)
    VALUES (@TableName, @Operation);
END;
