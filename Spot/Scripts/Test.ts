interface Post {
    title: string;
    author: string;
    content: string;
    status: number;
}

class Test {
    posts: Array<Post>;
    static connectionString: string;

    constructor(list: NodeList) {
        Test.connectionString = "adsfafd";
    }

    getString(): string {
        return "hello world";
    }

    static getStaticString(): string {
        return "static string";
    }

    addPost(post: Post): void {
        this.posts.push(post);
    }
}