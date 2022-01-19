-- create database
create database device_status_db;
create database rp_db;

use device_status_db;

-- create tb_status
create table tb_status(
    device_uuid varchar(36) primary key,
    status int(1),
    timeStamp varchar(25)
);


use rp_db;

-- create tb_device_info
create table tb_device_info(
    device_uuid varchar(36) primary key,
    agent_uuid varchar(36) NOT NULL,
    timeStamp varchar(25)
);

-- create tb_session_info
create table tb_session_info(
    session_uuid varchar(36) primary key,
    device_uuid varchar(36),
    foreign key(device_uuid) references tb_device_info(device_uuid)
);

-- create tb_msg_info
create table tb_msg_info(
    record_uuid varchar(36) primary key,
    session_uuid varchar(36),
    timeStamp varchar(25),
    data text,
    foreign key(session_uuid) references tb_session_info(session_uuid)
);

-- create tb_user_info
create table tb_user_info(
    -- id int NOT NULL AUTO_INCREMENT,
    id int primary key,
    userName varchar(255) NOT NULL UNIQUE,
    password varchar(255) NOT NULL
);

-- create table tb_user_info(
--     id int NOT NULL AUTO_INCREMENT,
--     username varchar(255) NOT NULL,
--     password_hash varbinary(64) NOT NULL,
--     password_salt varbinary(128) NOT NULL,
--     PRIMARY KEY (id)
-- );