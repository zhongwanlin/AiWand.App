projectname="aiwand";
def gitpath="https://github.com/zhongwanlin/AiWand.App.git";
workpath="/home/zhongwl01/project/AiWand";///share/wms-jenkins
def dllpath="/home/zhongwl01/project/AiWand/lib";//share/wms-jenkins/lib
version="v2.0";
   
applicationname="app";
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
    stage('构建镜像') {
         try{
            docker.withRegistry('https://registry.hub.docker.com','dockerhub') {
                def customImage = docker.build("${projectname}-${applicationname}:${version}"," ${mybuildpath}")
                    customImage.push();
            }
        }catch(e){
            throw e;
        }
    }

    stage('部署镜像') {
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
    try{
            docker.withRegistry('https://hub.docker.com/','dockerhub') {
                def image=docker.image("${projectname}-${applicationname}:${version}");
                image.pull();
                def runstr=" --name='${applicationname}' -p 80:5000 ";
                image.run(runstr);
            }	
        }catch(e){
        throw e;
    }
}