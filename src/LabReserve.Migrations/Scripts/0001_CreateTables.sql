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
