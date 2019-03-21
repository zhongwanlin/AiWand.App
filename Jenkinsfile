projectname="AiWand";
def gitpath="https://github.com/zhongwanlin/AiWand.App.git";
workpath="/root/project/dht/AiWand.App";///share/wms-jenkins
def dllpath="/root/project/dht/AiWand.App/lib";//share/wms-jenkins/lib
version="v2.1";
   
applicationname="App";
mybuildpath="${workpath}/src/AiWand.Api";

/////// 编译构建（主要工作编译程序，生成镜像，将镜像推送到私有仓）
node 
{
    stage('获取代码'){
        try{
            dir(workpath){
                git branch: "master",
                url: gitpath
           }
        }catch(e){
            echo "获取代码${e}";
            throw e;
        }
    }
    stage('编译'){
        dir(mybuildpath){
            sh '''rm bin/publish -rf
            dotnet publish -c Release -f netcoreapp2.1 -o bin/publish
            '''
        }
    }
    stage('构建') {
        docker.withRegistry('https://hub.docker.com/') {
            def customImage = docker.build("${projectname}/${applicationname}:${version}",
            "  --build-arg ENVIRONMENT=${mybuildpath}")
				customImage.push();
        }
    }

    stage('部署') {
        DropContainer();
        DeployApplication();
    }
}
///////// end

//销毁现有容器
def DropContainer(){
	try{
        sh '''docker stop ${applicationname}
        docker rm ${applicationname}'''
    }catch(e){
        echo "第一次构建${e}";
    }
}

//部署
def DeployApplication(){
        docker.withRegistry('https://hub.docker.com') {
            def image=docker.image("https://hub.docker.com/${projectname}/${applicationname}:${version}");
            image.pull();
            def runstr=" --name='${applicationname}' -p 80:80 ";
            image.run(runstr);
        }	
}