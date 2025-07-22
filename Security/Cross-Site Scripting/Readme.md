# Cross-Site Scripting (XSS)

| Rating         | Score                  |
| -------------- | ---------------------- |
| Prevalence     | :warning: 3 Common     |
| Fixability     | 🟢 5 (Easy to fix)     |
| Exploitability | 🔴 5 (Easy to exploit) |
| Impact         | 🌪️ 4 (Harmful)         |

---

### **Description**

Attackers inject malicious scripts (usually JavaScript) into web pages viewed by other users. It typically happens when untrusted user input is rendered directly into HTML without proper escaping or sanitization.

---

### **Risks**

- **Account hijacking** – stealing cookies or session tokens.
- **Phishing attacks** – injecting fake login forms.
- **Data theft** – reading sensitive data from the page.
- **Spreading malware** – injecting harmful scripts.

---

### **Protection**

1. **Always HTML-encode user input before displaying it** in the browser.
2. **Never insert raw user input into HTML**, JavaScript, or attributes.
3. **Use built-in encoding functions**, like `HttpUtility.HtmlEncode`.
4. **Apply Content Security Policy (CSP)** headers to restrict script execution.
5. **Validate input**, especially if used in templates or attributes.

---

### **Code Sample React**

#### ❌ Vulnerable Code (Do NOT do this):

React allows you write out raw HTML by binding content to the dangerouslySetInnerHTML property, which is named to remind you of the security risk! Watch out for any code that looks like the following:

```javascript
render() {
  return <div dangerouslySetInnerHTML={ __html: dynamicContent } />
}
```

---

#### ✅ Safe Code (Using HTML Encoding):

```javascript
render() {
  return <div>{dynamicContent}</div>
}
```

---
