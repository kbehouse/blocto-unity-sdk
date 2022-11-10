PRJ_FOLDER=/Users/kbe/unity/blocto-unity-sdk
PRJ_BUILD_FOLDER=${PRJ_FOLDER}/BuildTest
EDITOR_FOLDER=/Applications/Unity/Hub/Editor/
UNITY_PATH=/Unity.app/Contents/MacOS/Unity
[ -d ${PRJ_BUILD_FOLDER} ] || mkdir ${PRJ_BUILD_FOLDER}

# for launching Unity silently (see: https://docs.unity3d.com/2017.2/Documentation/Manual/CommandLineArguments.html)
[ -z "${UNITY_USERNAME}" ] && echo "Please export Unity username to ENV UNITY_USERNAME" && exit 1
[ -z "${UNITY_PASSWORD}" ] && echo "Please export Unity password to ENV UNITY_PASSWORD" && exit 1

declare -a versions=("2021.3.13f1" "2021.3.12f1" "2020.3.41f1" "2020.3.40f1" "2019.4.40f1" "2019.4.39f1")

for version in "${versions[@]}"
do
    # non silently method
    # ${EDITOR_FOLDER}/${version}/${UNITY_PATH} -projectPath ${PRJ_FOLDER} -executeMethod BuildTest.BuildAndroid -logFile ${PRJ_BUILD_FOLDER}/${version}-android.log
    
    # Android
    echo "Build ${version} Android"
    ${EDITOR_FOLDER}/${version}/${UNITY_PATH} -projectPath ${PRJ_FOLDER} \
        -quit -batchmode -username ${UNITY_USERNAME} -password ${UNITY_PASSWORD} \
        -executeMethod BuildTest.BuildAndroid -buildTarget Android \
        -logFile ${PRJ_BUILD_FOLDER}/${version}-android.log ||  echo "\tbuild ${version} Android FAIL"
    # iOS
    echo "Build ${version} iOS"
    ${EDITOR_FOLDER}/${version}/${UNITY_PATH} -projectPath ${PRJ_FOLDER} \
        -quit -batchmode -username ${UNITY_USERNAME} -password ${UNITY_PASSWORD} \
        -executeMethod BuildTest.BuildIOS -buildTarget iOS \
        -logFile ${PRJ_BUILD_FOLDER}/${version}-iOS.log || echo "\tbuild ${version} iOS FAIL"
done

