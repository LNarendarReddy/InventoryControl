// ignore_for_file: file_names

import 'package:flutter/material.dart';
import 'package:nsretail/Screens/branchList.dart';
import 'package:nsretail/Screens/stockCountingList.dart';
import 'package:nsretail/Screens/stockDispatchList.dart';
import 'package:shared_preferences/shared_preferences.dart';

import '../login.dart';

class InitialPage extends StatefulWidget {
  int branchId;
  String branchName;
  String userName;
  InitialPage(this.branchId, this.branchName, this.userName);

  @override
  State<InitialPage> createState() => _InitialPageState();
}

class _InitialPageState extends State<InitialPage> {
  SharedPreferences sharedPreferences;
  @override
  void initState() {
    // TODO: implement initState
    super.initState();
    checkLoginState();
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
        title: Text('Victory Bazars'),
      ),
      // drawer: Drawer(
      //   // Add a ListView to the drawer. This ensures the user can scroll
      //   // through the options in the drawer if there isn't enough vertical
      //   // space to fit everything.
      //   child: ListView(
      //     // Important: Remove any padding from the ListView.
      //     padding: EdgeInsets.zero,
      //     children: [
      //       const DrawerHeader(
      //         decoration: BoxDecoration(
      //           color: Colors.blue,
      //         ),
      //         child: Image(
      //           image: AssetImage('images/logo.png'),
      //         ),
      //       ),
      //       ListTile(
      //         title: const Text('Branches'),
      //         onTap: () {
      //           Navigator.of(context).pushAndRemoveUntil(
      //               MaterialPageRoute(
      //                   builder: (BuildContext context) =>
      //                       BranchList()),
      //                   (route) => false);
      //           //Navigator.pop(context);
      //         },
      //       ),
      //       ListTile(
      //         title: const Text('Logout   Ver 1.1.0'),
      //         onTap: () {
      //           sharedPreferences.clear();
      //           sharedPreferences.setString("token", "");
      //           sharedPreferences.remove("token");
      //           checkLoginState();
      //           print('logout pressed');
      //
      //         },
      //       ),
      //     ],
      //   ),
      // ),
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.start,
          crossAxisAlignment: CrossAxisAlignment.end,
          children: [
            // Row(
            //   mainAxisAlignment: MainAxisAlignment.center,
            //   crossAxisAlignment: CrossAxisAlignment.end,
            //   children: [
            //     Text('User Name:'),
            //     SizedBox(
            //       width: 30,
            //     ),
            //     Text(_loginUserName)
            //   ],
            // ),
            // SizedBox(
            //   height: 30,
            // ),
            Row(
              mainAxisAlignment: MainAxisAlignment.center,
              crossAxisAlignment: CrossAxisAlignment.end,
              children: [
                Text('Branch Name:'),
                SizedBox(
                  width: 30,
                ),
                Text(widget.branchName)
              ],
            ),
            SizedBox(
              height: 30,
            ),
            Row(
              mainAxisAlignment: MainAxisAlignment.center,
              crossAxisAlignment: CrossAxisAlignment.end,
              children: [
                Card(
                    child: InkWell(
                  onTap: () {
                    Navigator.of(context).pushAndRemoveUntil(
                        MaterialPageRoute(
                            builder: (BuildContext context) =>
                                stockCountingList(
                                    widget.branchId, widget.branchName, widget.userName)),
                        (route) => false);
                  },
                  child: Ink.image(
                    child: Text('Stock Counting'),
                    image: AssetImage('images/stockcounting.jpg'),
                    height: 150,
                    width: 150,
                  ),
                )),
                // SizedBox(
                //   width: 30,
                // ),
                // Card(
                //     child: InkWell(
                //   onTap: () {
                //     Navigator.of(context).pushAndRemoveUntil(
                //         MaterialPageRoute(
                //             builder: (BuildContext context) => stockDispatchList(widget.branchId)),
                //             (route) => false);
                //   },
                //   child: Ink.image(
                //     child: Text('Stock Dispatch'),
                //     image: AssetImage('images/stockdispatch.jpg'),
                //     height: 150,
                //     width: 150,
                //   ),
                // )),
              ],
            ),
            SizedBox(
              height: 30,
            ),
            // Row(
            //   mainAxisAlignment: MainAxisAlignment.center,
            //   crossAxisAlignment: CrossAxisAlignment.end,
            //   children: [
            //     Card(
            //         child: InkWell(
            //       onTap: () {},
            //       child: Ink.image(
            //         child: Text('Stock Entry'),
            //         image: AssetImage('images/stockentry.png'),
            //         height: 150,
            //         width: 150,
            //       ),
            //     )),
            //     SizedBox(
            //       width: 30,
            //     ),
            //     Card(
            //         child: InkWell(
            //       onTap: () {},
            //       child: Ink.image(
            //         child: Text('Billing'),
            //         image: AssetImage('images/billing.jpg'),
            //         height: 150,
            //         width: 150,
            //       ),
            //     )),
            //   ],
            // )
          ],
        ),
      ),
    );
  }
}
