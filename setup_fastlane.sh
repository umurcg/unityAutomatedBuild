#!/bin/bash
BUILD_PATH=$1
echo "Build Path: $BUILD_PATH"

# Echo the paths of the P12 file and provisioning profile
echo "P12 File Path: $FASTLANE_P12_FILE"
echo "Provisioning Profile Path: $FASTLANE_PROVISIONING_PROFILE_PATH"


FASTLANE_DIR="$BUILD_PATH/fastlane"

# Import the P12 file into the keychain
echo "Importing P12 File: $FASTLANE_P12_FILE"
security import "$FASTLANE_P12_FILE" -k ~/Library/Keychains/login.keychain-db -P 123456 -T /usr/bin/codesign

# Verify the import of P12
echo "Verifying P12 import:"
security find-identity -p codesigning -v

# Copy the provisioning profile to the correct directory
echo "Copying provisioning profile: $FASTLANE_PROVISIONING_PROFILE_PATH"
cp "$FASTLANE_PROVISIONING_PROFILE_PATH" ~/Library/MobileDevice/Provisioning\ Profiles/

# Verify the copy of the provisioning profile
echo "Copied provisioning profiles:"
ls ~/Library/MobileDevice/Provisioning\ Profiles/

# Setup Fastlane directory and Fastfile if not exists
if [ ! -d "$FASTLANE_DIR" ]; then
    mkdir -p "$FASTLANE_DIR"
    touch "$FASTLANE_DIR/Fastfile"

    echo "default_platform(:ios)" >> "$FASTLANE_DIR/Fastfile"
    echo "" >> "$FASTLANE_DIR/Fastfile"
    echo "platform :ios do" >> "$FASTLANE_DIR/Fastfile"
    echo "  desc 'Deploy the app to TestFlight/App Store'" >> "$FASTLANE_DIR/Fastfile"
    echo "  lane :deploy do" >> "$FASTLANE_DIR/Fastfile"
    echo "    gym(export_method: 'app-store', project: 'Unity-iPhone.xcodeproj', scheme: 'Unity-iPhone', output_directory: 'builds/')" >> "$FASTLANE_DIR/Fastfile"
    echo "    upload_to_app_store(skip_metadata: true, skip_screenshots: true, skip_binary_upload: false)" >> "$FASTLANE_DIR/Fastfile"
    echo "  end" >> "$FASTLANE_DIR/Fastfile"
    echo "end" >> "$FASTLANE_DIR/Fastfile"
fi
