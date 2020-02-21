
var app = angular.module('app', ['ngFileUpload']);

app.controller('TransportCtrl', ['$scope', '$http', 'Upload', function ($scope, $http, Upload) {


    var userDetails = {};



    // to and then from  - used to put controller data into scope so can use in angular 
    $.extend($scope, window.controllerData.TransportModel);

    $scope.ClearItems = function () {
        $scope.Docs = [];
        $scope.$digest();
    }

    $scope.login = function () {
        $scope.RequestPending = true;
        var postData = {};
        postData.userAccessCode = $scope.userAccess;

        if (postData.userAccessCode === null || postData.userAccessCode === "") {
            $scope.UploadDocServerResponse = "Access Code not set";
            return;
        }

        $http.post('/Home/Login', postData).then(
            function (response) {

                $scope.RequestPending = false;
                if (response.data.length < 1) {
                    $scope.UploadDocServerResponse = "No Items for that Access code";
                    return;
                }

                for (var i = 0; i < response.data.length; i++) {
                    var d = {};
                    if (response.data[i].IsDoc) {
                        d.DocumentId = response.data[i].Doc.DocumentId;
                        d.DocumentName = response.data[i].Doc.DocumentName;

                    } else {
                        var t = response.data[i];
                        d.TransportText = t.TextItem.Value;
                    }
                   
                    d.IsDoc = response.data[i].IsDoc;
                   
                   

                    $scope.Docs.push(d);

                }
                $scope.setTimer();
             //   setTimeout(function () { $scope.ClearItems() }, 300000);
              //  $scope.GetMesages(response.data.Message);

            }
        );
    };

    
    $scope.Transport = function () {
        $scope.RequestPending = true;
        var postData = {};
        postData.userAccessCode = $scope.AccessCode;
        postData.Text = $scope.TextToSend;


        if (postData.userAccessCode === null || postData.userAccessCode === "") {
            $scope.UploadDocServerResponse = "Access Code not set";
            return;
        }

        if (postData.Text === null || postData.Text === "") {
            $scope.UploadDocServerResponse = "You have not typed any message to transport";
            return;
        }

        $http.post('/Home/Transport', postData).then(
            function (response) {
                $scope.RequestPending = false;
                alert(response.data.Message);
                if (response.data.WasSuccess === true) {
                    window.location.reload();
                }

                
               

            }
        );
    };

    $scope.GetMesages = function (userid) {
        $scope.RequestPending = true;
        var postData = {};
        postData.userId = userid;


        $http.post('/Home/GetMessagesForUser', postData).then(
            function (response) {
                $scope.RequestPending = false;
               

                for (var i = 0; i < response.data.length; i++) {
                    var message = {};
                    message.MessageId = response.data[i].MessageId;
                    message.IsDocument = response.data[i].IsDocument;
                    message.FileName = response.data[i].FileName;

                    $scope.Messages.push(message);

                }
                

            }
        );
    };


    $scope.CopyToClip = function share(value, index) {
        var text_to_share = value;

        // create temp element
        var copyElement = document.createElement("span");
        copyElement.appendChild(document.createTextNode(text_to_share));
        copyElement.id = 'tempCopyToClipboard';
        angular.element(document.body.append(copyElement));

        // select the text
        var range = document.createRange();
        range.selectNode(copyElement);
        window.getSelection().removeAllRanges();
        window.getSelection().addRange(range);

        // copy & cleanup
        document.execCommand('copy');
        window.getSelection().removeAllRanges();
        copyElement.remove();

        var id = index + "Item";
        document.getElementById(id).innerHTML = "Copied"; 
    };
    $scope.RandomAccessCode = function() {
        $scope.AccessCode = Math.floor((Math.random() * 100000000) + 1000);
    }
    $scope.submit = function () {

        if ($scope.AccessCode === null || $scope.AccessCode === "") {
            $scope.UploadDocServerResponse = "Access Code not set";
            return;
        }


        if ($scope.form.file.$valid && $scope.form.file) {



            if ($scope.file === undefined ) {
                $scope.UploadDocServerResponse = "File not selected";
                return;
            }
            $scope.upload($scope.file);
        } else {
            $scope.UploadDocServerResponse = "File Not selected";
            return;
        }
    };
    $scope.upload = function (file) {
        Upload.upload({
            url: '/Home/UploadDocument',
            data: { file: file, 'AccessCode': $scope.AccessCode }
        }).then(function (resp) {

            if (resp.data.WasSuccess === true) {
                alert(resp.data.Message);
                location.reload();
            } else {
                $scope.UploadDocServerResponse = resp.data.Message;
            }

            //console.log('Success ' + resp.config.data.file.name + 'uploaded. Response: ' + resp.data);
        }, function (resp) {
           // console.log('Error status: ' + resp.status);
        }, function (evt) {
          //  var progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
           // console.log('progress: ' + progressPercentage + '% ' + evt.config.data.file.name);
        });
    };

    $scope.setTimer = function() {
        var d1 = new Date(),
            d2 = new Date(d1);
        d2.setMinutes(d1.getMinutes() + 5);
        var countDownDate1 = new Date(d2).getTime();
      //  log(countDownDate1);

      var countDownDate = new Date(d2).getTime();

      // Update the count down every 1 second
      var x = setInterval(function () {

          // Get today's date and time
          var now = new Date().getTime();

          // Find the distance between now and the count down date
          var distance = countDownDate - now;

          // Time calculations for days, hours, minutes and seconds
          var days = Math.floor(distance / (1000 * 60 * 60 * 24));
          var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
          var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
          var seconds = Math.floor((distance % (1000 * 60)) / 1000);

         //  Output the result in an element with id="demo"
          document.getElementById("demo").innerHTML = 
              minutes + "m " + seconds + "s ";
          
          //document.getElementsByClassName("counter").innerHTML =
          //    minutes + "m " + seconds + "s ";

          //var ele  = document.getElementsByClassName("counter");
          //for (var i = 0; i < ele.length; i++) {
          //    ele[i].innerHTML =
          //        minutes + "m " + seconds + "s ";
          //}

          // If the count down is over, write some text 
          if (distance < 0) {
              clearInterval(x);
              $scope.isExpired = true;
              $scope.$digest();
              document.getElementById("demo").innerHTML = "EXPIRED";
            //  document.getElementsByClassName("counter").innerHTML = "EXPIRED";
            

          }
      }, 1000);
    }

    


}]);


