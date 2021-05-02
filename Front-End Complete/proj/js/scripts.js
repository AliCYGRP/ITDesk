

function AddDevice(){
    let d=` <div class="col-xl-3 col-md-6 mb-4">
    <div class="card border-left-success shadow h-100 py-2">
      <div class="card-body">
        <div class="row no-gutters align-items-center">
          <div class="col mr-2">
            <div class="text-xs font-weight-bold text-success text-uppercase mb-1">
              Arpit</div>
            <div class="h5 mb-0 font-weight-bold text-gray-800">30 Units</div>
          </div>
          <div class="col-auto">
            <i class="fas fa-mobile fa-2x text-gray-300"></i>
          </div>
        </div>
      </div>
    </div>
  </div>`;
      
    document.getElementById('appendCard').innerHTML+=d;
  }



  function getFree(){
	fetch('https://localhost:44327/api/deviceinfo/employeeEmail',{
        mode: 'cors', // no-cors, *cors, same-origin
        cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
        credentials: 'same-origin', // include, *same-origin, omit
        headers: {
          'Content-Type': 'application/json'
          // 'Content-Type': 'application/x-www-form-urlencoded',
        },
        redirect: 'follow', // manual, *follow, error
        referrerPolicy: 'no-referrer'
    }
    )
    .then(res => res.json())
    .then(data => {
        let li = ` <tr>
        <th scope="col">Device Name</th>
        
        <th scope="col">IMEI </th>
        <th scope="col">Type</th>
      </tr>`;
                data.forEach(TempUser => {
                   // console.log(user);
                li += ` <tr>
                <td data-label="Account">${TempUser}}</td>
              
                <td data-label="Amount">$1,190</td>
                <td data-label="Period">03/01/2016 - 03/31/2016</td>
              </tr>`
				});

	document.getElementById('Table').innerHTML=li;
});
document.getElementById("free").style.backgroundColor="black";
document.getElementById("free").style.color="white";
document.getElementById("allocated").style.backgroundColor="gray";
}
function getAllocated(){
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
      let li = `
      <thead>
          <tr>
            <th scope="col">Device Name</th>
            <th scope="col">Allocated To</th>
            <th scope="col">IMEI </th>
            <th scope="col">Type</th>
          </tr>
        </thead>
      `;
      data.forEach((TempUser) => {
        // console.log(user);
        li += `  <tr>
        <td data-label="Account">${TempUser}}</td>
        <td data-label="Due Date">04/01/2016</td>
        <td data-label="Amount">$1,190</td>
        <td data-label="Period">03/01/2016 - 03/31/2016</td>
      </tr>`;
      });
      document.getElementById("Table").innerHTML = li;

      // do something with data
      console.log(data);
    })
    .catch(function (error) {
      console.log("Looks like there was a problem: \n", error);
    });
    document.getElementById("allocated").style.backgroundColor="black";
    document.getElementById("allocated").style.color="white";
    document.getElementById("free").style.backgroundColor="gray";
		}

    function searchFunction() {
      // Declare variables
      var input, filter, table, tr, td, i, txtValue;
      input = document.getElementById("myInput");
      filter = input.value.toUpperCase();
      table = document.getElementById("Table");
      tr = table.getElementsByTagName("tr");
    
      // Loop through all table rows, and hide those who don't match the search query
      for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[0];
        if (td) {
          txtValue = td.textContent || td.innerText;
          if (txtValue.toUpperCase().indexOf(filter) > -1) {
            tr[i].style.display = "";
          } else {
            tr[i].style.display = "none";
          }
        }
      }
    }

    function RequestDevice(){
      let d=`<div class="col-xl-3 col-md-6 mb-4">
      <div class="card border-left-success shadow h-100 py-2">
        <div class="card-body">
          <div class="row no-gutters align-items-center">
            <div class="col mr-2">
              <div class="text-xs font-weight-bold text-success text-uppercase mb-1">
                Mobile Name</div>
              <div class="h6 mb-0 font-weight-bold text-gray-800">Iphone 11 Mini</div>
            </div>
            <div class="col-auto">
              <i class="fas fa-mobile fa-2x text-gray-300"></i>
            </div>
          </div>
        </div>
        <a href="#" class="btn btn-danger m-2" data-target="#myModals" data-toggle="modal">
           
          <span class="text">Report Issue</span>
        </a>
        <a href="#" class="btn btn-secondary m-2">
           
          <span class="text">Return Device</span>
        </a>
      </div>
    </div>`;
        
      document.getElementById('appendCard').innerHTML+=d;
    }