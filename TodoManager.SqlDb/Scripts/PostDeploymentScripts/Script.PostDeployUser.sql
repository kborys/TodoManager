if not exists(select 1 from dbo.[User])
begin
    INSERT INTO dbo.[User](UserName, FirstName, LastName, [PasswordHash], EmailAddress)
    /* the passwords are set to "password" but are hashed*/
    VALUES 
        ('mastodont32', 'John', 'Rodriguez', 'gLIj2HltE+aKwgStTAvjrTeOpR1k0zlOu1lSkN6TlT4=:ZTSEZ6nvpQ8g28bHs2gFyg==:10000:SHA256        ' ,'example@gmail.com'),
        ('lara22', 'Lara', 'Rodriguez', 'gLIj2HltE+aKwgStTAvjrTeOpR1k0zlOu1lSkN6TlT4=:ZTSEZ6nvpQ8g28bHs2gFyg==:10000:SHA256        ' ,'lara22@gmail.com' );
end