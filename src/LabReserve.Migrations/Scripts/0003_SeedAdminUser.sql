IF NOT EXISTS (SELECT 1 FROM users WHERE email = 'admin@admin.com')
BEGIN
    INSERT INTO users (email, first_name, last_name, phone, password, user_type, created_by, created_date, status)
    VALUES ('admin@admin.com', 'Admin', 'User', '1234567890', 'a76b7f25b6ba5ec51bd9fa42f4143b63c2495996e783baa4d9f8459d314f6ad2', 3, 0, GETDATE(), 1);
END
