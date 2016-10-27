//: Playground - noun: a place where people can play

func makeReadLineMethod()->()->String? {
    var initStr:[String] = [

"100 100 100"
        
        
    ]
    var idx = -1;
    func proc()->String? {
        idx+=1
        return initStr[idx];
    }
    return proc;
}
var readLine = makeReadLineMethod();




extension String :CollectionType {}

var str = readLine()!;


