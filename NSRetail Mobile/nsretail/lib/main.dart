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
  bool loading = true;
  SharedPreferences sharedPreferences;
  String stUserID;
  String _loginUserName;

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
              'http://43.228.95.51/nsretailapi/api/users?Id= + ' + stUserID),
          headers: {"Authorization": "Bearer $value"});
      users = loadUsers(response.body);
      prefs.setString('userName', users[0].fullName);
      _loginUserName = users[0].fullName;

      setState(() {
        loading = false;
      });
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
    return parsed.map<Users>((json) => Users.fromJson(json)).toList();
  }

  @override
  void initState() {
    // TODO: implement initState
    super.initState();
    checkLoginState();
    this.getUserData();
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
        child: BranchList()
      ),
    );
  }
}
