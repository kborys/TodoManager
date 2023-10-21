if not exists(select 1 from dbo.Todo)
begin
    INSERT INTO dbo.[Todo](Title, [Description], GroupId, OwnerId, [Status])
    VALUES 
        ('TRASH!!!', 'Throw out the trash', 1, 1, 1),
        ('Homework', 'Prepare for discrete mathematics', 2, 1, 3),
        ('Do the shopping', 'eggs, bread, vinegar', 1, 2, 2);
end