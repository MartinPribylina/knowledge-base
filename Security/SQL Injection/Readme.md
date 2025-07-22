# SQL Injection

| Rating         | Score                  |
| -------------- | ---------------------- |
| Prevalence     | :warning: 3 Common     |
| Fixability     | 🟢 5 (Easy to fix)     |
| Exploitability | 🔴 5 (Easy to exploit) |
| Impact         | 🌪️ 5 (High impact)     |

---

### **Description**

User sends specially crafted input to manipulate a database query. It happens when user input is directly included in SQL statements without proper validation or escaping.

---

### **Risks**

- **Unauthorized data access** – attackers can view sensitive data.
- **Data manipulation** – attackers can insert, update, or delete records.
- **Database corruption or deletion** – via `DROP TABLE`, `DELETE`, etc.
- **Bypassing authentication** – logging in without valid credentials.

---

### **Protection**

1. **Use parameterized queries** (also known as prepared statements).
2. **Avoid string concatenation** for building SQL queries.
3. **Validate input** – ensure user input matches expected formats.
4. **Use ORM frameworks** like Entity Framework that handle SQL safely.
5. **Limit DB permissions** – use least privilege for DB accounts.

---

### **Code Sample C#**

#### ❌ Vulnerable Code (Do NOT do this):

```csharp
string username = txtUsername.Text;
string password = txtPassword.Text;

string query = $"SELECT * FROM Users WHERE Username = '{username}' AND Password = '{password}'";
SqlCommand cmd = new SqlCommand(query, connection);
SqlDataReader reader = cmd.ExecuteReader();
```

This code is vulnerable to injection like:
`username: ' OR 1=1 --`

#### ✅ Safe Code (Using Parameters):

```csharp
string username = txtUsername.Text;
string password = txtPassword.Text;

string query = "SELECT * FROM Users WHERE Username = @username AND Password = @password";
SqlCommand cmd = new SqlCommand(query, connection);
cmd.Parameters.AddWithValue("@username", username);
cmd.Parameters.AddWithValue("@password", password);
SqlDataReader reader = cmd.ExecuteReader();
```

#### ✅ Safe Code (Using LINQ):

Using Linq is superior in many ways.
[Optimized, Readable, Easy to use!](https://www.youtube.com/watch?v=FTN_SBLmDMM)

```csharp
string username = txtUsername.Text;
string password = txtPassword.Text;

using (var context = new MyDbContext())
{
    var user = context.Users
        .Where(u => u.Username == username && u.Password == password)
        .FirstOrDefault();

    if (user != null)
    {
        // Login success
    }
    else
    {
        // Invalid login
    }
}

```

This prevents SQL Injection because user input is treated as data, **not executable code**.

---
