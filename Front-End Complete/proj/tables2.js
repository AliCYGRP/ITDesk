




  function FetchData()
{
    var myHeaders = new Headers();
    myHeaders.append("Authorization", "Bearer "+localStorage.getItem(""));

  var requestOptions = {
    headers: myHeaders,
    method: 'GET',
    redirect: 'follow'
  };
 
      fetch("https://localhost:44327/api/deviceinfo/employeeEmail", requestOptions)
        .then(response => response.json())
        .then(result => {
          var StrGet="";
          
          result.forEach((user)=>{
            // console.log(user.address)
            StrGet += ` <tr>
                                                          <td>
                                                            <a href="#">
                                                              <div class="d-flex align-items-center">
                                                                <div class="avatar avatar-pink mr-3">JR</div>
                                              
                                                                <div class="">
                                                                  <p class="font-weight-bold mb-0">Julie Richards</p>
                                                                  <p class="text-muted mb-0">julie_89@example.com</p>
                                                                </div>
                                                              </div>
                                                            </a>
                                                          </td>
                                                          <td> (937) 874 6878</td>
                                                          <td>Investment Banker</td>
                                                          <td>13/01/1989</td>
                                                          <td>
                                                            <div class="badge badge-success badge-success-alt">Enabled</div>
                                                          </td>
                                                          <td>
                                                            <div class="dropdown">
                                                              <button class="btn btn-sm btn-icon" type="button" id="dropdownMenuButton2" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                                    <i class="bx bx-dots-horizontal-rounded" data-toggle="tooltip" data-placement="top"
                                                                      title="Actions"></i>
                                                                  </button>
                                                              <div class="dropdown-menu" aria-labelledby="dropdownMenuButton2">
                                                                <a class="dropdown-item" href="#"><i class="bx bxs-pencil mr-2"></i> Edit Profile</a>
                                                                <a class="dropdown-item text-danger" href="#"><i class="bx bxs-trash mr-2"></i> Remove</a>
                                                              </div>
                                                            </div>
                                                          </td>
                                                        </tr>`;
            // create list and bind
            // similarly do for rest functions
             document.getElementById("infoDevices").innerHTML=StrGet;
          });

        })
        .catch(error => console.log('error', error));

    }

    function free() {
        fetch("https://localhost:44327/api/deviceinfo/employeeEmail", {
          mode: "cors", // no-cors, *cors, same-origin
          cache: "no-cache", // *default, no-cache, reload, force-cache, only-if-cached
          credentials: "same-origin", // include, *same-origin, omit
          headers: {
            "Content-Type": "application/json",
            Authorization:
              "Bearer " + window.localStorage.getItem("token").toString(),
            // 'Content-Type': 'application/x-www-form-urlencoded',
          },
          redirect: "follow", // manual, *follow, error
          referrerPolicy: "no-referrer",
        })
          .then((res) => res.json())
          .then((data) => {
            let li = "";
            data.forEach((TempUser) => {
              // console.log(user);
              li += `  <tr>
              <td data-label="Account">${TempUser}}</td>
              <td data-label="Due Date">04/01/2016</td>
              <td data-label="Amount">$1,190</td>
              <td data-label="Period">03/01/2016 - 03/31/2016</td>
            </tr>`;
            });
            document.getElementById("infoDevices").innerHTML = li;
      
            // do something with data
            console.log(data);
          })
          .catch(function (error) {
            console.log("Looks like there was a problem: \n", error);
          });
      }