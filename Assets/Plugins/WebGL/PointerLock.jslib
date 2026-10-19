mergeInto(LibraryManager.library, {
    WebGL_RequestPointerLock: function () {
        try {
            var canvas = document.querySelector("#unity-canvas") || document.querySelector("canvas");
            if (canvas && canvas.requestPointerLock) {
                // OSのマウス加速処理をバイパスし、生の移動量を取得してデルタスパイクを抑制
                var promise = canvas.requestPointerLock({ unadjustedMovement: true });
                if (promise && promise.catch) {
                    promise.catch(function (err) {
                        // unadjustedMovementが未対応のブラウザでは通常のオプションなしで再試行
                        var fallbackPromise = canvas.requestPointerLock();
                        if (fallbackPromise && fallbackPromise.catch) {
                            fallbackPromise.catch(function (fallbackErr) {
                                console.warn("[PointerLock.jslib] requestPointerLock failed:", fallbackErr);
                            });
                        }
                    });
                }
            }
        } catch (e) {
            console.warn("[PointerLock.jslib] requestPointerLock exception:", e);
        }
    },

    WebGL_ExitPointerLock: function () {
        try {
            if (document.exitPointerLock && document.pointerLockElement) {
                document.exitPointerLock();
            }
        } catch (e) {
            console.warn("[PointerLock.jslib] exitPointerLock exception:", e);
        }
    },

    WebGL_IsPointerLocked: function () {
        return (document.pointerLockElement != null);
    }
});
