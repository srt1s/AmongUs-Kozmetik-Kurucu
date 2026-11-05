@echo off
chcp 65001 >nul
setlocal enabledelayedexpansion
title Among Us - Kozmetik Menüsü Kurucu

echo ================================================================
echo       AMONG US - KOZMETİK MENÜSÜ OTOMATİK KURUCU
echo ================================================================
echo.

set "TARGET_DIR="

:: 1. Epic Games Manifest Taramasi
if exist "C:\ProgramData\Epic\EpicGamesLauncher\Data\Manifests" (
    for %%F in ("C:\ProgramData\Epic\EpicGamesLauncher\Data\Manifests\*.item") do (
        findstr /i "AmongUs" "%%F" >nul 2>&1
        if !errorlevel! equ 0 (
            for /f "tokens=2 delims=:" %%A in ('findstr /i "InstallLocation" "%%F"') do (
                set "LOC=%%A"
                set "LOC=!LOC:~2!"
                set "LOC=!LOC:~0,-2!"
                set "LOC=!LOC:\\=\!"
                if exist "!LOC!\Among Us.exe" (
                    set "TARGET_DIR=!LOC!"
                    echo [OK] Epic Games Among Us tespit edildi: !TARGET_DIR!
                    goto found
                )
            )
        )
    )
)

:: 2. Standart Epic Games Dizinleri
if exist "C:\Program Files\Epic Games\AmongUs\Among Us.exe" (
    set "TARGET_DIR=C:\Program Files\Epic Games\AmongUs"
    goto found
)
if exist "D:\Epic Games\AmongUs\Among Us.exe" (
    set "TARGET_DIR=D:\Epic Games\AmongUs"
    goto found
)
if exist "E:\Epic Games\AmongUs\Among Us.exe" (
    set "TARGET_DIR=E:\Epic Games\AmongUs"
    goto found
)
if exist "D:\Program Files\Epic Games\AmongUs\Among Us.exe" (
    set "TARGET_DIR=D:\Program Files\Epic Games\AmongUs"
    goto found
)

:: 3. Standart Steam Dizinleri
if exist "C:\Program Files (x86)\Steam\steamapps\common\Among Us\Among Us.exe" (
    set "TARGET_DIR=C:\Program Files (x86)\Steam\steamapps\common\Among Us"
    goto found
)
if exist "D:\SteamLibrary\steamapps\common\Among Us\Among Us.exe" (
    set "TARGET_DIR=D:\SteamLibrary\steamapps\common\Among Us"
    goto found
)

:prompt_user
echo [!] Oyun klasoru otomatik olarak bulunamadi.
set /p "TARGET_DIR=Lutfen Among Us klasorunun tam yolunu yapistirin: "
if not exist "%TARGET_DIR%\Among Us.exe" (
    echo.
    echo [UYARI] Bu klasorde Among Us.exe bulunamadi!
    echo Klasor: %TARGET_DIR%
    echo.
)

:found
echo.
echo Kurulum hedefi: "%TARGET_DIR%"
echo.

:: Hedef oyunun 64-bit mi 32-bit mi oldugunu kontrol et
set "ARCH=x86"
echo %TARGET_DIR% | findstr /i "Epic" >nul 2>&1
if %errorlevel% equ 0 (
    set "ARCH=x64"
)

set "SOURCE_DIR=%~dp0Dosyalar\%ARCH%"
if not exist "%SOURCE_DIR%\BepInEx" (
    set "SOURCE_DIR=%~dp0Dosyalar"
)

echo Secilen Paket: %ARCH% (Kaynak: %SOURCE_DIR%)
echo Dosyalar kopyalaniyor...
xcopy "%SOURCE_DIR%\*" "%TARGET_DIR%\" /E /I /Y /Q >nul

echo.
echo ================================================================
echo   [TEBRIKLER] Kurulum basariyla tamamlandi!
echo ================================================================
echo.
echo Nasil calistirilir?
echo  1. Epic Games Launcher (veya Steam) uzerinden oyunu acin.
echo  2. Oyun acilinca klavyenizden [DELETE] tusuna basin.
echo  3. Acilan menuden 'Tum Kozmetikleri Ac' secenegi ile tum
echo     kostum, sapka, pet ve vizorleri ucretsiz kullanabilirsiniz!
echo.
pause
