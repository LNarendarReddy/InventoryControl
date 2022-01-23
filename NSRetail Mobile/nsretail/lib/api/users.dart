class Users {
  int userId;
  String fullName;
  String userName;
  String email;
  int gender;
  int branchId;
  String branchName;
  bool isWarehouse;

  Users({this.userId, this.fullName, this.userName, this.email, this.gender,this.branchId,this.branchName,this.isWarehouse});
  factory Users.fromJson(Map<String, dynamic> parsed) {
    return Users(
      userId: parsed["userid"],
      fullName: parsed["fullname"] as String,
      userName: parsed["username"] as String,
      email: parsed["email"] as String,
      gender: parsed["gender"],
      branchId: parsed["branchid"],
      branchName: parsed["branchname"] as String,
      isWarehouse: parsed["iswarehouse"]
    );
  }
}
