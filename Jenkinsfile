pipeline {
    agent any
    
    environment {
        DOTNET_VERSION = '8.0'  // Adjust this to match your .NET Core SDK version
        MSBUILD_PATH = "C:\\Program Files (x86)\\Microsoft Visual Studio\\2019\\BuildTools\\MSBuild\\Current\\Bin\\MSBuild.exe"  // Path to MSBuild
        IIS_SITE_NAME = 'IDPService8002'  // Replace with your existing IIS site name
        PUBLISH_DIR = "C:\\Jenkins\\workspace\\IDPService8002\\publish"  // Path to publish directory
        NETWORK_PATH = "\\\\192.168.3.12\\publish_root"  // Network path
        DEPLOY_DIR = "${NETWORK_PATH}\\IDPService8002"  // Deployment directory on the network
        DRIVE_LETTER = "F:"
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

                    // Map the network drive with credentials
                    bat "net use ${DRIVE_LETTER} ${NETWORK_PATH} /user:%NETWORK_USERNAME% %NETWORK_PASSWORD% /persistent:no"
                    
                    // Clean up existing deployment directory and create new one
                    bat """
                        if exist ${DRIVE_LETTER}\\IDPService8002 (
                            rmdir /S /Q ${DRIVE_LETTER}\\IDPService8002
                        )
                        mkdir ${DRIVE_LETTER}\\IDPService8002
                    """
                    
                    // Deploy to existing IIS site
                    bat "xcopy /Y /S ${PUBLISH_DIR}\\* ${DRIVE_LETTER}\\IDPService8002\\"
                    
                    // Unmap the network drive
                    bat "net use ${DRIVE_LETTER} /delete"
                    
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
