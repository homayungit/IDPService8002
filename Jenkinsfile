pipeline {
    agent any

    environment {
        DOTNET_VERSION = '8.0'  // Adjust this to match your .NET Core SDK version
        PUBLISH_DIR = "C:\\Jenkins\\workspace\\IDPService8002\\publish"  // Path to publish directory
        NETWORK_PATH = "\\\\192.168.3.12\\publish_root"  // Network path \\192.168.3.12\publish_root\IDPService8002
        DRIVE_LETTER = "F:"
        USERNAME = 'admin.homayun'
        PASSWORD = 'H%M@k87!hameem'
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
                    bat 'xcopy /Y /S C:\\Jenkins\\workspace\\IDPService8002\\publish\\* D:\PUBLISH_ROOT\IDPService8002\\'
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
