

function fileQueued(file)
{
    try
        {
            //alert(swfu.flash_url);
            var p = new FileProgress(file, swfu.settings.custom_settings.progressTarget);
            fg_fileSizes += file.size;
            p.setShow(true);
    }
    catch (e)
        {
            this.debug(e);
    }
}

function fileDialogComplete()
{
    //fg_fileSizes = 0;
    fg_uploads = 0;
    fg_object = new FileGroupProgress();
    fg_object.setFileCountSize(fg_fileSizes);
    swfu.startUpload();
}

function fileQueueError(file, errorCode, message)
{
    try
    {
        if (errorCode === SWFUpload.QUEUE_ERROR.QUEUE_LIMIT_EXCEEDED)
        {
            //alert("You have attempted to queue too many files.\n" + (message === 0 ? "You have reached the upload limit." : "You may select " + (message > 1 ? "up to " + message + " files." : "one file.")));

            top.$.alert('You choose too many files！', 'Operation tips');
            return;
        }

        var progress = new FileProgress(file, swfu.settings.custom_settings.progressTarget);
        //progress.setError();
        progress.setShow(false);

        fg_fileSizes -= file.size;
        fg_object.setFileCountSize(fg_fileSizes);

        switch (errorCode)
        {
            case SWFUpload.QUEUE_ERROR.FILE_EXCEEDS_SIZE_LIMIT:
                //progress.setStatus("File is too big.");
                alert("File is too big.");
                this.debug("Error Code: File too big, File name: " + file.name + ", File size: " + file.size + ", Message: " + message);
                break;
            case SWFUpload.QUEUE_ERROR.ZERO_BYTE_FILE:
                //progress.setStatus("Cannot upload Zero Byte files.");
                alert("Cannot upload Zero Byte files.");
                this.debug("Error Code: Zero byte file, File name: " + file.name + ", File size: " + file.size + ", Message: " + message);
                break;
            case SWFUpload.QUEUE_ERROR.INVALID_FILETYPE:
                //progress.setStatus("Invalid File Type.");
                alert("Invalid File Type.");
                this.debug("Error Code: Invalid File Type, File name: " + file.name + ", File size: " + file.size + ", Message: " + message);
                break;
            default:
                if (file !== null)
                {
                    progress.setStatus("Unhandled Error");
                }
                alert("Unhandled Error!");
                this.debug("Error Code: " + errorCode + ", File name: " + file.name + ", File size: " + file.size + ", Message: " + message);
                break;
        }
    } catch (ex)
    {
        this.debug(ex);
    }
}

function uploadStart(file)
{
    try
    {
        /* I don't want to do any file validation or anything,  I'll just update the UI and
        return true to indicate that the upload should start.
        It's important to update the UI here because in Linux no uploadProgress events are called. The best
        we can do is say we are uploading.
        */
        var progress = new FileProgress(file, swfu.settings.custom_settings.progressTarget);
        progress.setUploadState(3,this.settings);
        //progress.toggleCancel(true, swfu);
    }
    catch (ex) { }

    return true;
}

function uploadProgress(file, bytesLoaded, bytesTotal)
{
    try
    {
        var percent = Math.ceil((bytesLoaded / bytesTotal) * 100);

        var progress = new FileProgress(file, swfu.settings.custom_settings.progressTarget);
        //progress.setProgress(percent);
        progress.setProgress(percent);

        //fg_uploads += bytesLoaded;

        fg_object.setUploadProgress(fg_uploads+bytesLoaded, fg_fileSizes);
    } catch (ex)
    {
        this.debug(ex);
    }
}

function uploadSuccess(file, serverData)
{
    try
    {
        var progress = new FileProgress(file, swfu.settings.custom_settings.progressTarget);
        progress.setComplete(this.settings);
        fg_uploads += file.size;
        if (-1 == serverData) {
            top.$.alert('Upload Failed！', 'Operation tips');
            return false;
        }
        else if (-2 == serverData) {
            top.$.alert('Please select a file to upload！', 'Operation tips');
            return false;
        }
        else if (-3 == serverData) {
            top.$.alert('File uploads unusual！', 'Operation tips');
            return false;
        } else {
           
            var obj = $.parseJSON(serverData);
            $(".js_delete").each(function () {
                var objId = $(this).attr("id");
                var objFname = $.trim($(this).attr("accesskey"));
                if (objFname == obj.fileName)
                {
                    $("#sp_fname_SWFUpload_0_" + objId.substring(14)).html("<a target=\"_blank\" href=\"/upload/" + obj.fullname + "\">" + objFname + "</a>");
                    $("#" + objId).attr("fullname", obj.fullname);                    
                    $("#imgCar").attr("src","/upload/" + obj.fullname);
                    return;
                }
            });
            var strFileName = $("#hdFileNmae").val();
            $("#hdFileNmae").val(strFileName+obj.fullname);
        }
       
        //progress.setStatus("Complete.");
        //progress.toggleCancel(false,swfu);
        //swfu.startUpload();

    } catch (ex)
    {
        this.debug(ex);
    }
}

function uploadComplete(file)
{
    try
        {
            //swf.stratUpload();
    }
    catch (ex)
        {
            this.debug(ex);
    }
}

function uploadError(file, errorCode, message)
{
    try
    {
        var progress = new FileProgress(file, swfu.settings.custom_settings.progressTarget);
        progress.setShow(false);
        fg_fileSizes -= file.size;
        fg_object.setFileCountSize(fg_fileSizes);

        switch (errorCode)
        {
            case SWFUpload.UPLOAD_ERROR.HTTP_ERROR:
                //progress.setStatus("Upload Error: " + message);
                alert("Upload Error:" + message);
                this.debug("Error Code: HTTP Error, File name: " + file.name + ", Message: " + message);
                break;
            case SWFUpload.UPLOAD_ERROR.UPLOAD_FAILED:
                //progress.setStatus("Upload Failed.");
                alert("Upload Failed!");
                this.debug("Error Code: Upload Failed, File name: " + file.name + ", File size: " + file.size + ", Message: " + message);
                break;
            case SWFUpload.UPLOAD_ERROR.IO_ERROR:
                //progress.setStatus("Server (IO) Error");
                alert("IO Error!");
                this.debug("Error Code: IO Error, File name: " + file.name + ", Message: " + message);
                break;
            case SWFUpload.UPLOAD_ERROR.SECURITY_ERROR:
                //progress.setStatus("Security Error");
                alert("Security Error!");
                this.debug("Error Code: Security Error, File name: " + file.name + ", Message: " + message);
                break;
            case SWFUpload.UPLOAD_ERROR.UPLOAD_LIMIT_EXCEEDED:
                //progress.setStatus("Upload limit exceeded.");
                alert("Upload Limit Exceeded!");
                this.debug("Error Code: Upload Limit Exceeded, File name: " + file.name + ", File size: " + file.size + ", Message: " + message);
                break;
            case SWFUpload.UPLOAD_ERROR.FILE_VALIDATION_FAILED:
                //progress.setStatus("Failed Validation.  Upload skipped.");
                alert("File Validation Failed!");
                this.debug("Error Code: File Validation Failed, File name: " + file.name + ", File size: " + file.size + ", Message: " + message);
                break;
            case SWFUpload.UPLOAD_ERROR.FILE_CANCELLED:
                //progress.setStatus("Cancelled");
                //alert("上传被终止!");
                //progress.setCancelled();
                break;
            case SWFUpload.UPLOAD_ERROR.UPLOAD_STOPPED:
                //progress.setStatus("Stopped");
                //alert("上传被停止!");
                break;
            default:
                //progress.setStatus("Unhandled Error: " + errorCode);
                alert("Unhandled Error,ErrorCode:" + errorCode);
                this.debug("Error Code: " + errorCode + ", File name: " + file.name + ", File size: " + file.size + ", Message: " + message);
                break;
        }
    } catch (ex)
    {
        this.debug(ex);
    }
}

// This event comes from the Queue Plugin
function queueComplete(numFilesUploaded)
{
    /*var status = document.getElementById("divStatus");
    status.innerHTML = numFilesUploaded + " file" + (numFilesUploaded === 1 ? "" : "s") + " uploaded.";*/
}