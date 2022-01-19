-- create database
create database selCourse_db;

use selCourse_db;

-- create tb_teacher
create table tb_teacher(
    tch_uuid varchar(36) primary key,
    name varchar(30),
    major varchar(20) NOT NULL,
    timeStamp varchar(25)
);

-- create tb_student
create table tb_student(
    stu_uuid varchar(36) primary key,
    name varchar(30),
    tch_uuid varchar(36),
    foreign key(tch_uuid) references tb_teacher(tch_uuid)
);

-- create tb_course
create table tb_course(
    crs_Seq int NOT NULL AUTO_INCREMENT,
    stu_uuid varchar(36),
    crsName varchar(40),
    timeStamp varchar(25),
    remark text,
    PRIMARY KEY (crs_Seq),
    foreign key(stu_uuid) references tb_student(stu_uuid)
);

-- create tb_user
-- create table tb_user(
--     id int primary key,
--     userName varchar(255) NOT NULL UNIQUE,
--     password varchar(255) NOT NULL
-- );

create table tb_user(
    id int NOT NULL AUTO_INCREMENT,
    userName varchar(50) NOT NULL UNIQUE,
    pwdHash varbinary(64) NOT NULL,
    pwdSalt varbinary(128) NOT NULL,
    PRIMARY KEY (id)
);