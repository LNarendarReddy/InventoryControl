import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:nsretail/Screens/branchList.dart';
import 'package:nsretail/login.dart';
import 'package:http/http.dart' as http;
import 'dart:io';
import 'dart:convert';
import 'package:autocomplete_textfield/autocomplete_textfield.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:nsretail/api/branches.dart';
import 'package:nsretail/api/users.dart';

import 'Screens/stockCountingList.dart';

void main() {
  HttpOverrides.global = new MyHttpOverrides();
  runApp(const MyApp());
}

class MyHttpOverrides extends HttpOverrides {
  @override
  HttpClient createHttpClient(SecurityContext context) {
    // TODO: implement createHttpClient
    return super.createHttpClient(context)
      ..badCertificateCallback =
          (X509Certificate cert, String host, int port) => true;
  }
}

Future<bool> addSelfSignedCertificate() async {
  ByteData data = await rootBundle.load('assets/3.122.143.49.crt');
  SecurityContext context = SecurityContext.defaultContext;
  context.setTrustedCertificatesBytes(data.buffer.asUint8List());

  return true;
}

class MyApp extends StatelessWidget {
  const MyApp({Key key}) : super(key: key);

  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Victory Bazar',
      theme: ThemeData(
        primarySwatch: Colors.blue,
      ),
      home: const MyHomePage(title: 'Victory Bazar'),
    );
  }
}

class MyHomePage extends StatefulWidget {
  const MyHomePage({Key key, this.title}) : super(key: key);
  final String title;

  @override
  State<MyHomePage> createState() => _MyHomePageState();
}

class _MyHomePageState extends State<MyHomePage> {
  static List<Users> users = new List<Users>();
  static List<Branches> _branches = new List<Branches>();
  bool loading = true;
  SharedPreferences sharedPreferences;
  String stUserID;
  String _loginUserName;
  bool _isData = false;

  void getUserData() async {
    try {
      SharedPreferences prefs = await SharedPreferences.getInstance();
      //Return String
      String value = prefs.getString('token') ?? "";
      stUserID = prefs.getString('userID') ?? "";

      print('userid');
      print(stUserID);
      final response = await http.get(
          Uri.parse(
              'http://103.195.186.197/nsretailapi/api/users?Id= + ' + stUserID),
          headers: {"Authorization": "Bearer $value"});
      users = loadUsers(response.body);
      prefs.setString('userName', users[0].fullName);
      _loginUserName = users[0].fullName;
      print('User Name: ' + _loginUserName);

      setState(() {
        loading = false;
        _loginUserName = users[0].fullName;
      });
      //this.getStockCountData();
    } catch (e) {
      print(e.toString());
    }
  }

  static List<Users> loadUsers(String jsonString) {
    print('loadusers');

    print(jsonString);
    Map<String, dynamic> map = json.decode(jsonString);
    final parsed = map["data"];
    print(parsed);
    //final parsed=json.decode(jsonString).cast<Map<String,dynamic>>();
    if(parsed!=null) {
      return parsed.map<Users>((json) => Users.fromJson(json)).toList();
    }
    else {
      return [];
    }
  }

  void getStockCountData() async {
    try {
      SharedPreferences prefs = await SharedPreferences.getInstance();
      //Return String
      String value = prefs.getString('token') ?? "";
      stUserID = prefs.getString('userID') ?? "";

      print('stock count userid');
      print(stUserID);
      final response = await http.get(
          Uri.parse(
              'http://103.195.186.197/nsretailapi/api/StockCounting/GetStockCount/' +
                  stUserID),
          headers: {"Authorization": "Bearer $value"});
      _branches = loadStockCount(response.body);
      print('branches');
      print(_branches);
      //print('Branches length ' + _branches.length.toString());
      setState(() {
        loading = false;
        if (_branches.length>0) {
          _isData = true;
        } else {
          _isData = false;
        }
      });
      print('isdata' + _isData.toString());
    } catch (e) {
      print(e.toString());
    }
  }

  static List<Branches> loadStockCount(String jsonString) {
    print('load stockcount data');

    print(jsonString);
    Map<String, dynamic> map = json.decode(jsonString);
    final parsed = map["data"];
    print(parsed);
    //final parsed=json.decode(jsonString).cast<Map<String,dynamic>>();
    if(parsed!=null) {
      return parsed.map<Branches>((json) => Branches.fromJson(json)).toList();
    }
    else {
      return [];
    }
  }

  @override
  void initState() {
    // TODO: implement initState
    super.initState();
    checkLoginState();
    this.getUserData();
    this.getStockCountData();
  }

  checkLoginState() async {
    sharedPreferences = await SharedPreferences.getInstance();
    if (sharedPreferences.getString('token') == null) {
      Navigator.of(context).pushAndRemoveUntil(
          MaterialPageRoute(builder: (BuildContext context) => Login()),
          (route) => false);
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Center(
          child: _isData
              ? stockCountingList(_branches[0].branchId,
                  _branches[0].branchName, _loginUserName)
              : BranchList(_loginUserName)),
    );
  }
}
