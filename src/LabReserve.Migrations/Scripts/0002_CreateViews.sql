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
