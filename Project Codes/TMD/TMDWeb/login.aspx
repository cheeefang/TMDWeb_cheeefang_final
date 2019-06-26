<%@ Page Language="C#" AutoEventWireup="true" codeFile="login.aspx.cs" Inherits="targeted_marketing_display.login" %>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">
    <title>Targeted Marketing Display</title>
    
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
    
    <style>
        body {
             
           background-attachment:fixed;
            background-repeat: no-repeat;
            margin: 0;
            padding: 0;
            background-size:100%;
            background-size: cover;
            font-family: sans-serif;
             
             
        }
        .login-box {
            width: 300px;
            height: 350px;
            top: 50%;
            left: 50%;
            position: absolute;
            transform: translate(-50%,-50%);
            color: white;
            background-color: #BFB7B6;
            padding: 40px;
           border-radius:20px;
        }

        

        .login-box h1 {
            float:left;
            font-size:40px;
            border-bottom:6px solid #435058;
            margin-bottom:50px;
            padding:12px 0;
            margin-top:10px;

        }
        .textbox {
        width:100%;
        overflow:hidden;
        font-size:20px;
        padding:8px 0;
        margin:8px 0;
        border-bottom:1px solid #DCF763;
        }
            .textbox i {
            width:26px;
            float:left;
            text-align:center;
            }
            .textbox input {
            border:none;
            outline:none;
            background:none;
            color:white;
            font-size:18px;
            width:80%;
            float:left;
            margin:0 10px;

            }

        .btn {
          width:100%;
          background:#DCF763;
          border:2px solid #DCF763;
          color:black;
          padding:5px;
          font-size:18px;
          cursor:pointer;
          margin:12px 0;
          border-radius:5px;
            }
       .etc-login-form {
  color: antiquewhite ;
  padding: 10px 20px;
}
.etc-login-form p {
  margin-bottom: 5px;
}
        </style> 
     
</head>
    <body style="background-image: url('Images/billboard.gif');">
    <form id="form2" runat="server">
    <div class="login-box">
         
         
        <h1>Login Here</h1>
        <div class="textbox">
            <i class="fa fa-user" aria-hidden="true"></i>
            <asp:TextBox ID="unTB" placeholder="username" runat="server"></asp:TextBox>

            </div>
        <div class="textbox"> 
            <i class="fa fa-lock" aria-hidden="true"></i>
           <asp:TextBox ID="pwTB" placeholder="password" runat="server" TextMode="Password"></asp:TextBox>
        </div>
        
        <br />
        <asp:Button ID="loginBtn" class="btn btn-primary nextBtn pull-right" runat="server" Font-Bold="true" Text="Login" OnClick="login_onclick" style="text-align:center; color: #435058;background-color: #dcf763;border-color: #dcf763;" />
        	<div class="etc-login-form">
              

				<p>Forgot your password? <a href="#">click here</a></p>
				 
			</div>
         </div>
    
        
       </form>
    </body>

</html>
