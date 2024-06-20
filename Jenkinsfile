pipeline {
    agent any
    
    environment {
        DOTNET_VERSION = '8.0'  // Adjust this to match your .NET Core SDK version
        MSBUILD_PATH = "C:\\Program Files (x86)\\Microsoft Visual Studio\\2019\\BuildTools\\MSBuild\\Current\\Bin\\MSBuild.exe"  // Path to MSBuild
        IIS_SITE_NAME = 'IDPService8002'  // Replace with your existing IIS site name
        PUBLISH_DIR = "C:\\Jenkins\\workspace\\IDPService8002\\publish"  // Path to publish directory
    }
    
    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }
        
        stage('Restore Dependencies') {
            steps {
                bat "dotnet restore"
            }
        }
        
        stage('Build') {
            steps {
                bat "dotnet build --configuration Release"
            }
        }
        
        stage('Publish') {
            steps {
                bat "dotnet publish --configuration Release --output ${PUBLISH_DIR}"
            }
        }
        stage('Deploy to IIS') {
            steps {
                script {
                    // Stop the IIS site (if already running)
                    bat "iisreset /stop"
                    
                    bat '''
                        if exist E:\\TUTORIALS\\DeployPath\\IDPService8002 (
                            rmdir /S /Q E:\\TUTORIALS\\DeployPath\\IDPService8002
                        )
                        mkdir E:\\TUTORIALS\\DeployPath\\IDPService8002
                    '''
                    
                    // Deploy to existing IIS site
                    //bat 'xcopy /Y /S C:\\Jenkins\\workspace\\IDPService8002\\publish\\* E:\\TUTORIALS\\DeployPath\\IDPService8002\\'
                    bat 'xcopy /Y /S C:\\Jenkins\\workspace\\IDPService8002\\publish\\* \\\\192.168.3.12\\publish_root\\IDPService8002\\'
                    
                    // Start the IIS site
                    bat "iisreset /start"
                }
            }
        }

    }
    
    post {
        success {
            echo 'Deployment to IIS completed successfully!'
        }
        failure {
            echo 'Deployment to IIS failed.'
        }
    }
}
