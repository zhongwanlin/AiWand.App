projectname="wholesale";
def gitpath="http://tututech.f3322.net:8000/dotnet/wholesale.git";
workpath="/root/project/dht/wholesale";///share/wms-jenkins
def dllpath="/root/project/dht/wholesale/lib";//share/wms-jenkins/lib
version="v2.1";
   
applicationname="${params.applicationname}";//wechatappapi
multimachine="${params?.multimachine}";//是否多机部署（0否、1是）
gitbranch="${params.gitbranch}";//test-jenkins
evn="${params.evn}";//Development/Staging/Production
evnlowercase="${params.evn}".toLowerCase();
mybuildpath="${workpath}/${params.buildpath}";//  /share/wms/src/Api/WMS.WebApi
myapplicationtype="${applicationtype}".toLowerCase();//Console/Web
myapplcationpoint=GetApplicationPoint(myapplicationtype);
buildnode=GetBuildNode(evn);

/////// 编译构建（主要工作编译程序，生成镜像，将镜像推送到私有仓）
node (buildnode)
{
    stage('获取代码'){
       dir(workpath){
           git branch: gitbranch,
           url: gitpath
       }
       dir(workpath){
            sh '''git submodule init
            git submodule update'''
       }
    }
    stage('编译'){
        dir(mybuildpath){
            sh '''rm bin/publish -rf
            dotnet publish -c Release -f netcoreapp2.1 -o bin/publish
            '''
        }
		dir(workpath){
			sh 'cp lib/ServiceStack.OrmLite.dll ${buildpath}/bin/publish -f'
		}
    }
    stage('构建') {
        docker.withRegistry('https://hub.zhidianlife.com') {
            def customImage = docker.build("${projectname}/${applicationname}-${evnlowercase}:${version}",
            "  --build-arg ENVIRONMENT=${evn} ${mybuildpath}")
				customImage.push();
        }
    }
}
///////// end

///////// 部署（主要工作是从私有仓或者本地获取镜像，通过镜像启动程序）
if(evn=='Development'){
    node ('for_ubuntu_16'){
        stage('部署'){
            DropContainer();
            DeployApplication();
        }
    }
}
if(evn=='Staging'){
  node ('for_alpha_centos_16')
  {
      stage('部署'){
          DropContainer();
          DeployApplication();
      }
  }
}
if(evn=='Production'){
  node ("for_Ubuntu_223"){
    stage('部署'){
		DropContainer();
		DeployApplication();
    }
  }
  if(myapplicationtype=='web'||multimachine=='1'){
    node ("for_Ubuntu_224"){
        stage('部署'){
		    DropContainer();
		    DeployApplication();
        }
    }  
  }
}
//////// end

//////// 函数
//获取应用端口
def GetApplicationPoint(type){
	if(type=='web'){
		"${applcationpoint}";
	}else{
		"";
	}
}
//获取编译机器标签
def GetBuildNode(environmental){
	if(environmental=='Development'){
		buildnode="for_ubuntu";
	}else{
		buildnode="for_alpha_centos_16";
	}
	return buildnode;
}
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
        docker.withRegistry('https://hub.zhidianlife.com') {
            def image=docker.image("hub.zhidianlife.com/${projectname}/${applicationname}-${evnlowercase}:${version}");
            image.pull();
            def runstr='';
			if(myapplicationtype=='web'){
				runstr=" --name='${applicationname}' -p ${myapplcationpoint}:80 ";
			}else{
				runstr=" --name='${applicationname}' ";
			}
            image.run(runstr);
        }	
}