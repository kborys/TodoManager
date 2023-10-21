if not exists(select 1 from dbo.[UserGroupRelation])
begin
    INSERT INTO dbo.[UserGroupRelation](UserId, GroupId)
    VALUES 
        (1, 2),
        (1, 1),
        (2, 1);
end