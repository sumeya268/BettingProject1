<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <title>Slips – Login</title>
    <link rel="stylesheet" href="styles.css">
</head>

<body class="auth-body">

    <!-- Toast Notification -->
    <div id="toast" class="toast"></div>

    <div class="auth-container">
        <div class="auth-box">

            <!-- LOGIN SUCCESS MESSAGE -->
            <div id="loginSuccess" class="login-success" style="display:none;">
                Login successful!
            </div>

            <h1 class="auth-title">Welcome back</h1>

            <form id="loginForm">

                <label class="auth-label">Username</label>
                <input class="auth-input" type="text" id="loginUsername" required>

                <label class="auth-label">Password</label>
                <input class="auth-input" type="password" id="loginPassword" required>

                <button type="submit" class="auth-button">Log In</button>
            </form>

            <p class="auth-switch">
                New to Slips?
                <a href="signup.html">Create an account</a>
            </p>

        </div>
    </div>

   
    <script>
        window.PAGE_TYPE = "login";
    </script>

    <script src="login.js"></script>

</body>
</html>
