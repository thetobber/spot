var Test = (function () {
    function Test(list) {
        Test.connectionString = "adsfafd";
    }
    Test.prototype.getString = function () {
        return "hello world";
    };
    Test.getStaticString = function () {
        return "static string";
    };
    Test.prototype.addPost = function (post) {
        this.posts.push(post);
    };
    return Test;
}());
//# sourceMappingURL=Test.js.map