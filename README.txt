LOGIN APPLICATION
C# Windows Forms (.NET Framework 4.8) / SQL Server

SETUP
1. Extract the entire ZIP file.
2. Open SQL Server Management Studio and connect to your SQL Server instance using Windows Authentication.
3. Open Database.sql in SSMS and execute the whole script.
4. Open LoginApplication.sln in Visual Studio. If prompted, install the .NET desktop development workload and .NET Framework 4.8 targeting pack.
5. In UserDatabase.cs, change localhost\SQLEXPRESS01 to your SQL Server instance if needed. The database name is LoginDB.
6. Press F5 to run. The form controls are created in C# code, so you do not need to add controls manually or use the designer.

TESTING
1. Create username student1 with password Test1234 and matching confirmation. A success message should appear.
2. Create student1 again. A duplicate username warning should appear.
3. Enter different passwords in Password and Confirm Password. A validation message should appear.
4. Try logging in with student1 and a wrong password. Login should fail.
5. Log in with student1 and Test1234. The Logged In form should appear.
6. Click Logout to return to the login form. Log in again and click Exit to close the application.
7. Run SELECT UserID, Username, PasswordHash, PasswordSalt FROM dbo.Users; in SSMS with LoginDB selected. Only the username, hash, and salt are stored, not the original password.
Use fictional test credentials rather than real passwords.

PASSWORD SECURITY
Passwords are salted and hashed with PBKDF2-HMAC-SHA256, using 600,000 iterations, a random 16-byte salt, and a 32-byte hash. Hashing is one-way, rather than reversible encryption. SQL statements use parameters; a unique constraint prevents duplicate usernames.
