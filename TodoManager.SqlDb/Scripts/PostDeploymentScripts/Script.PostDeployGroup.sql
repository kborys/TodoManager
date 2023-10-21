if not exists(select 1 from dbo.[Group])
begin
    INSERT INTO dbo.[Group]([Name], OwnerId)
        VALUES 
            ('University', 1),
            ('Home', 2);
end