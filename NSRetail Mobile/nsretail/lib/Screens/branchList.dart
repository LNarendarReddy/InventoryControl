// ignore_for_file: file_names

import 'dart:convert';
import 'package:http/http.dart' as http;
import 'package:autocomplete_textfield/autocomplete_textfield.dart';
import 'package:flutter/material.dart';
import 'package:nsretail/Screens/InitialPage.dart';
import 'package:nsretail/Screens/stockCountingList.dart';
import 'package:nsretail/api/branches.dart';
import 'package:nsretail/api/users.dart';
import 'package:shared_preferences/shared_preferences.dart';

import '../login.dart';

class BranchList extends StatefulWidget {
  String userName;
  BranchList(this.userName);
  @override
  _BranchListState createState() => _BranchListState();
}

class _BranchListState extends State<BranchList> {
  AutoCompleteTextField searchTextField;
  GlobalKey<AutoCompleteTextFieldState<Branches>> key = new GlobalKey();
  static List<Branches> _branchesList = new List<Branches>();
  List<Branches> _filteredBranchList = new List<Branches>();
  static List<Users> users = new List<Users>();
  bool loading = true;
  SharedPreferences sharedPreferences;
  String stUserID;
  final String url = 'http://103.195.186.197/nsretailapi/api/branch';

  void getData() async {
    try {
      SharedPreferences prefs = await SharedPreferences.getInstance();
      //Return String
      String value = prefs.getString('token') ?? "";
      stUserID = prefs.getString('userID') ?? "";

      print('userid');
      print(stUserID);
      final response = await http
          .get(Uri.parse(url), headers: {"Authorization": "Bearer $value"});
      _branchesList = loadBranches(response.body);
      _filteredBranchList = _branchesList;
      print('Branches: ${_branchesList.length}');
      // print(_filteredBranchList[1].branchName);
      setState(() {
        loading = false;
      });
    } catch (e) {
      print(e.toString());
    }
  }

  static List<Branches> loadBranches(String jsonString) {
    print('loadbranches');

    print(jsonString);
    Map<String, dynamic> map = json.decode(jsonString);
    final parsed = map["data"];
    print(parsed);
    if (parsed != null)
    //final parsed=json.decode(jsonString).cast<Map<String,dynamic>>();
    {
      return parsed.map<Branches>((json) => Branches.fromJson(json)).toList();
    } else {
      return [];
    }
  }

  @override
  void initState() {
    // TODO: implement initState
    super.initState();
    checkLoginState();
    this.getData();
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
      appBar: AppBar(
        title: Text('Branches'),
        actions: [
          new Container(),
          new Center(
            child: Text(
              widget.userName,
            ),
          )

          // Icon(Icons.person,color: Colors.white,),
        ],
      ),
      drawer: Drawer(
        // Add a ListView to the drawer. This ensures the user can scroll
        // through the options in the drawer if there isn't enough vertical
        // space to fit everything.
        child: ListView(
          // Important: Remove any padding from the ListView.
          padding: EdgeInsets.zero,
          children: [
            const DrawerHeader(
              decoration: BoxDecoration(
                color: Colors.blue,
              ),
              child: Image(
                image: AssetImage('images/logo.png'),
              ),
            ),
            ListTile(
              title: const Text('Logout   Ver 1.1.0'),
              onTap: () {
                sharedPreferences.clear();
                sharedPreferences.setString("token", "");
                sharedPreferences.remove("token");
                checkLoginState();
                print('logout pressed');
              },
            ),
          ],
        ),
      ),
      body: Center(
        child: Column(
          children: [
            TextField(
              decoration: InputDecoration(
                contentPadding: EdgeInsets.all(10.0),
                hintText: 'Enter Branch Name',
              ),
              onSubmitted: (string) {},
              onChanged: (string) {
                setState(() {
                  _filteredBranchList = _branchesList
                      .where((element) => (element.branchName
                          .toLowerCase()
                          .contains(string.toLowerCase())))
                      .toList();
                });
              },
            ),
            Expanded(
              child: ListView.builder(
                  padding: EdgeInsets.all(10.0),
                  itemCount: _filteredBranchList.length != null
                      ? _filteredBranchList.length
                      : 0,
                  itemBuilder: (BuildContext context, int index) {
                    return ListTile(
                      title: Text(_filteredBranchList[index].branchCode == null
                          ? "Branch"
                          : _filteredBranchList[index].branchName),
                      trailing: Icon(Icons.arrow_forward),
                      onTap: () {
                        Navigator.push(
                          context,
                          MaterialPageRoute(
                            builder: (context) => InitialPage(
                                _filteredBranchList[index].branchId,
                                _filteredBranchList[index].branchName,
                                widget.userName),
                          ),
                        );
                      },
                    );
                  }),
            ),
            SizedBox(
              height: 20,
            ),
          ],
        ),
      ),
    );
  }
}
