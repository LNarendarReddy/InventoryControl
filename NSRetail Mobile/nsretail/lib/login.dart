import 'dart:convert';
import 'package:http/http.dart' as http;
import 'package:flutter/material.dart';
import 'package:nsretail/main.dart';
import 'package:shared_preferences/shared_preferences.dart';

class Login extends StatefulWidget {
  @override
  _LoginState createState() => _LoginState();
}

class _LoginState extends State<Login> {
  bool _isLoading = false;
  String _error = "";
  Container errorLabel(String error) {
    return Container(
      child: Text(error),
    );
  }

  Center headersection() {
    return Center(
      child: Text(
        'Victory Bazars',
        style: TextStyle(
            color: Colors.lightBlue, fontWeight: FontWeight.bold, fontSize: 24),
      ),
    );
  }

  TextEditingController emailcontroller = new TextEditingController();
  TextEditingController pwdcontroller = new TextEditingController();
  final FocusNode _emailfocus = FocusNode();
  final FocusNode _passwordfocus = FocusNode();
  final FocusNode _submitfocus = FocusNode();

  TextFormField txtEmail(String title, IconData icon) {
    return TextFormField(
        textInputAction: TextInputAction.next,
        controller: emailcontroller,
        style: TextStyle(color: Colors.lightBlue),
        focusNode: _emailfocus,
        onFieldSubmitted: (term) {
          _fieldFocusChange(context, _emailfocus, _passwordfocus);
        },
        decoration: InputDecoration(
          hintText: title,
          hintStyle: TextStyle(color: Colors.lightBlue),
          icon: Icon(
            icon,
            color: Colors.lightBlue,
          ),
        ));
  }

  TextFormField txtPassword(String title, IconData icon) {
    return TextFormField(
        textInputAction: TextInputAction.done,
        focusNode: _passwordfocus,
        controller: pwdcontroller,
        obscureText: true,
        style: TextStyle(color: Colors.lightBlue),
        onFieldSubmitted: (term) {
          _fieldFocusChange(context, _passwordfocus, _submitfocus);
        },
        decoration: InputDecoration(
          hintText: title,
          hintStyle: TextStyle(color: Colors.lightBlue),
          icon: Icon(
            icon,
            color: Colors.lightBlue,
          ),
        ));
  }

  Container buttonsection() {
    return Container(
      width: MediaQuery.of(context).size.width,
      height: 40.0,
      margin: EdgeInsets.only(top: 30.0),
      padding: EdgeInsets.symmetric(horizontal: 20.0),
      child: RaisedButton(
        focusNode: _submitfocus,
        onPressed: () {
          setState(() {
            //_isLoading = true;
          });
          if (emailcontroller.text == "") {
            _error = 'Please enter UserName';
            return;
          }
          if (pwdcontroller.text == "") {
            _error = 'Please enter Password';
            return;
          }
          signIn(emailcontroller.text, pwdcontroller.text);
        },
        color: Colors.lightBlue,
        shape: RoundedRectangleBorder(
          borderRadius: BorderRadius.circular(5.0),
        ),
        child: Text(
          'SignIn',
          style: TextStyle(color: Colors.white, fontSize: 16),
        ),
      ),
    );
  }

  _fieldFocusChange(
      BuildContext context, FocusNode currentFocus, FocusNode nextFocus) {
    currentFocus.unfocus();
    FocusScope.of(context).requestFocus(nextFocus);
  }

  Container textsection() {
    return Container(
      padding: EdgeInsets.symmetric(horizontal: 20.0, vertical: 30.0),
      margin: EdgeInsets.only(top: 30.0),
      child: Column(
        children: [
          txtEmail("User Name", Icons.email),
          SizedBox(height: 30.0),
          txtPassword("Password", Icons.lock),
        ],
      ),
    );
  }

  signIn(String email, String pwd) async {
    Map data = {'grant_type': 'password', 'username': email, 'Password': pwd};
    var jsondata;
    String tokenvalue;
    String userID;
    String userName;
    SharedPreferences sharedPreferences = await SharedPreferences.getInstance();
    var url = 'http://43.228.95.51/nsretailapi/token';

    var response = await http.post(Uri.parse(url), body: data);
    if (response.statusCode == 200) {
      print(response.body);
      jsondata = json.decode(response.body);
      setState(() {
        _isLoading = false;
        tokenvalue = jsondata['access_token'].toString();
        userID = jsondata['UserId'].toString();
        print(userID);
        print('token value');
        print(tokenvalue);
        print(userID);
        sharedPreferences.setString("token", tokenvalue);
        sharedPreferences.setString("userID", userID);
        if (userID == null || userID == '') {
          _error = 'Invalid Credentials';
        } else {
          Navigator.of(context).pushAndRemoveUntil(
              MaterialPageRoute(
                  builder: (BuildContext context) => MyHomePage()),
              (route) => false);
        }
      });
    } else {
      print(response.body);
      _error = 'Invalid credentials';
      return;
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        body: Container(
      decoration: BoxDecoration(
          gradient: LinearGradient(colors: [
        Colors.white,
        Colors.white,
      ], begin: Alignment.topCenter, end: Alignment.bottomCenter)),
      child: ListView(
        children: [
          SizedBox(
            height: 50,
          ),
          Image.asset(
            'images/logo.png',
            height: 150,
          ),
          //SizedBox(height: 10,),
          //headersection(),
          textsection(),
          buttonsection(),
          SizedBox(height: 30,),
          Center(
            child: Text(_error, style: TextStyle(color: Colors.red,fontSize: 14),),
          )

        ],
      ),
    ));
  }
}
