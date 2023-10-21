if not exists(select 1 from dbo.[Status])
begin
    INSERT INTO dbo.[Status]([Name])
    VALUES ('Deleted'),
           ('Pending'),
           ('DoToday'),
           ('InProgress'),
           ('Done');
end