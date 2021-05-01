function CheckLogin(){
    if(localStorage.getItem("token")==null)
    {
        location.replace("./index.html");
    }
}

function load(){
    window.localStorage.removeItem("token");
}

function LoginUser()
{
    var user= document.getElementById("user_login");
    var pass= document.getElementById("password_login");

    var myHeaders = new Headers();
    myHeaders.append("Content-Type", "application/json");
    
    var raw = JSON.stringify({
      "EmailAddress":user.value,
      "Password": pass.value
    });

    var requestOptions = {
      method: 'POST',
      headers: myHeaders,
      body: raw,
      redirect: 'follow'
    };
    
    fetch("https://localhost:44327/api/login", requestOptions)
      .then(response => response)
      .then(result => {
        if(result.token=="")
        {
            alert("OOPS! Wrong Email Or Password!")
        }  
        else
        {
           
            if(user.value == 'abhinav.mishra@cygrp.com')
            {
                localStorage.setItem("token",result.token);
                location.replace("./first.html")
            }
            else
            {
                localStorage.setItem("token",result.token);
                location.replace("./second.html")
            
        }

        }
        })
      .catch(error => console.log(error));
}


function login() {
    var username = document.getElementById("user_login").value;
    var pass = document.getElementById("password_login").value;
    
    if(username==="")
    {
        document.getElementById("vi").innerHTML="Enter Valid Email";
        document.getElementById("vi").style.color="Red";

    }
    else 
    {
      if(username.indexOf("@")> -1)
      {
          document.getElementById("vi").innerHTML="";
      }
      else{
        document.getElementById("vi").innerHTML="Enter The Correct Email Address";
                    document.getElementById("vi").style.color="Red";
      }
    }
    if(pass.length <=0)
    {
      document.getElementById("vi2").innerHTML="Enter The Password";
    }
    else{
      if(pass.length<=6)
                {
                    document.getElementById("vi2").innerHTML="Password Is Wrong";
                    document.getElementById("vi2").style.color="Red";
                }
                 else
                {
                    document.getElementById("vi2").innerHTML="";
                }
    }
    
    
    var loginInfo = {
      EmployeeEmail: username,
      Password: CryptoJS.MD5(pass).toString(),
    };
  
    console.log(loginInfo);
    fetch("https://localhost:44327/api/login", {
      method: "POST",
      mode: "cors", // no-cors, *cors, same-origin
      cache: "no-cache", 
      credentials: "same-origin", 
      headers: {
        "Content-Type": "application/json",
        
      },
      redirect: "follow", 
      referrerPolicy: "no-referrer",
      body: JSON.stringify(loginInfo),
    })
      .then((response) => response.text())
      .then((response) => {
        console.log("Bearer " + response);
        var obj= JSON.parse(response);
      
        window.localStorage.setItem("token", obj.tokenString);
        if(obj.tokenString.length != 0){
            if(obj.role == true){
                openAdmin();
            }
            
            else{
                openEmployee();
            }
        }
        else{
          

          if(obj.tokenString == null)
          {
           document.getElementById("vi2").innerHTML="";
          
          }
          else if(pass.length==0){
            document.getElementById("vi2").innerHTML="Password Required";
          }
          else{
           
            document.getElementById("vi2").innerHTML="Wrong Credentials Email/Password ";
           
          }
        }
        
      });
  }


function logout(){
    localStorage.removeItem("result-token");
    location.replace("../index.html");
}
function openAdmin() {
    location.replace("../Admin.html")
  }
  function openEmployee() {
    location.replace("../Employee.html")
  }