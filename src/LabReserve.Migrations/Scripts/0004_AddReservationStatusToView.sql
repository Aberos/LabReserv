create or alter view v_reserves
as
select  r.id             as idReserve,
        r.status         as statusReserve,
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

create index ix_reservations_room_period on reservations (id_room, period_start, period_end) where status = 1;
