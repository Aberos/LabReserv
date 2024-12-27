create database labreserve
go

use labreserve
go

create table users
(
    id              bigint          not null primary key identity,
    status          int,
    email           varchar(250) not null unique,
    first_name      varchar(50)  not null,
    last_name       varchar(50)  not null,
    phone           varchar(20)  null,
    password        varchar(max)  not null,
    user_type       int,
    created_by      bigint not null,
    created_date    datetime not null,
    updated_by      bigint,
    updated_date    datetime,
    check (status in (1,2,3,4)),
    check (user_type in (1,2,3))
);

create table rooms
(
    id              bigint not null primary key identity,
    name            varchar(50)  not null,
    status          int,
    created_by      bigint not null,
    created_date    datetime not null,
    updated_by      bigint,
    updated_date    datetime,
    check (status in (1,2,3,4))
);

create table courses
(
    id              bigint not null primary key identity,
    name            varchar(50)  not null,
    status          int,
    created_by      bigint not null,
    created_date    datetime not null,
    updated_by      bigint,
    updated_date    datetime,
    check (status in (1,2,3,4))
);

create table groups
(
    id              bigint not null primary key identity,
    name            varchar(50)  not null,
    id_course       bigint not null references courses(id),
    status          int,
    created_by      bigint not null,
    created_date    datetime not null,
    updated_by      bigint,
    updated_date    datetime,
    check (status in (1,2,3,4))
);

create table group_user
(
    id_user         bigint not null references users(id),
    id_group        bigint not null references groups(id),
    status          int not null,
    created_by      bigint not null,
    created_date    datetime not null,
    updated_by      bigint,
    updated_date    datetime,
    check (status in (1,2,3,4)),
    primary key(id_user, id_group)
);

create table reservations
(
    id              int not null primary key identity,
    period_start    datetime not null,
    period_end      datetime,
    id_room         bigint not null references rooms(id),
    id_user         bigint not null references users(id),
    id_group        bigint not null references groups(id),
    status          int,
    created_by      bigint not null,
    created_date    datetime not null,
    updated_by      bigint,
    updated_date     datetime,
    check (status in (1,2,3,4))
);

create table requests
(
    id              int not null primary key identity,
    id_user         bigint not null references users(id),
    id_room         bigint not null references rooms(id),
    id_group        bigint not null references groups(id),
    period_start    datetime not null,
    period_end      datetime,
    status          int not null,
    created_by      bigint not null,
    created_date    datetime not null,
    updated_by      bigint,
    updated_date    datetime,
    check (status in (1,2,3,4))
);

GO

create view v_groups
as
select g.id              as idGroup,
       g.name            as nameGroup,
       g.status          as statusGroup,
       c.id              as idCourse,
       c.name            as nameCourse,
       c.status          as statusCourse
from groups g
         inner join courses c on c.id = g.id_course;

GO

create view v_reserves
as
select  r.id             as idReserve,
        r.period_start   as periodStart,
        r.period_end     as periodEnd,
        ro.id            as idRoom,
        ro.name          as nameRoom,
        ro.status        as statusRoom,
        c.id             as idCreator,
        c.first_name     as firstNameCreator,
        c.last_name      as lastNameCreator,
        c.status         as statusCreator,
        u.id             as idUser,
        u.first_name     as firstNameUser,
        u.last_name      as lastNameUser,
        u.user_type      as userType,
        u.status         as statusUser,
        g.idGroup        as idGroup,
        g.nameGroup      as nameGroup,
        g.statusGroup    as statusGroup,
        g.idCourse       as idCourse,
        g.nameCourse     as nameCourse,
        g.statusCourse   as statusCourse
from reservations r
         inner join rooms ro on ro.id = r.id_room
         inner join users c on c.id = r.created_by
         inner join users u on u.id = r.id_user
         inner join v_groups g on g.idGroup = r.id_group;

GO

create view v_group_user
as
select g.id              as idGroup,
       g.name            as nameGroup,
       g.status          as statusGroup,
       c.id              as idCourse,
       c.name            as nameCourse,
       c.status          as statusCourse,
       u.id              as idUser,
       u.status          as statusUser,
       gu.status         as statusGroupUser
from group_user gu
         inner join groups g on g.id = gu.id_group
         inner join courses c on c.id = g.id_course
         inner join users u on u.id = gu.id_user;

GO

create view v_request
as
select  r.id             as idRequest,
        u.id             as idUser,
        u.first_name     as firstNameUser,
        u.last_name      as lastNameUser,
        u.user_type      as userType,
        u.status         as statusUser,
        ro.id            as idRoom,
        ro.name          as nameRoom,
        ro.status        as statusRoom,
        g.idGroup        as idGroup,
        g.nameGroup      as nameGroup,
        g.statusGroup    as statusGroup,
        g.idCourse       as idCourse,
        g.nameCourse     as nameCourse,
        g.statusCourse   as statusCourse,
        r.period_start   as periodStart,
        r.period_end     as periodEnd,
        r.status         as statusRequest
from requests r
         inner join users u on u.id = r.id_user
         inner join rooms ro on ro.id = r.id_room
         inner join v_groups g on g.idGroup = r.id_group;

GO

IF NOT EXISTS (SELECT 1 FROM users WHERE email = 'admin@admin.com')
BEGIN
    INSERT INTO users (email, first_name, last_name, phone, password, user_type, created_by, created_date, status)
    VALUES ('admin@admin.com', 'Admin', 'User', '1234567890', 'a76b7f25b6ba5ec51bd9fa42f4143b63c2495996e783baa4d9f8459d314f6ad2', 3, 0, GETDATE(), 1);
END
GO